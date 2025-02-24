using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Http;
//using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FIPAG_KOBOTOOLBOX.Domains.Interface;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.DTOs;
using FIPAG_KOBOTOOLBOX.Extensions;

using FIPAG_KOBOTOOLBOX.Persistence.APIs;
using FIPAG_KOBOTOOLBOX.Helper;
/*
using FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.Octopi;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.Octopi.Responses;
using FIPAG_KOBOTOOLBOX.Services.External;
*/
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using FIPAG_KOBOTOOLBOX.Persistence.Repositories;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;
using Z.EntityFramework.Plus;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox;
using AutoMapper.Features;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Globalization;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.KoboToolBox.DTOs;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.Khulipa;
using FIPAG_KOBOTOOLBOX.Persistence.APIs.Khulipa.DTOs;

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class KhulipaService : IKhulipaService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KulipaAPI koboAPI = new KulipaAPI();


        //private readonly IPHCRepository _PHCRespository;
        private readonly IPHCMainRepository<AppDbContextMain> _phcMainRepository;
        private readonly IPHCRepository2<DynamicContext> _phcDynamicRepository;
        private readonly IGenericRepository<AppDbContextMain> _genericRepositoryOnBD;

        public KhulipaService(IPHCMainRepository<AppDbContextMain> Main_Repository, IPHCRepository2<DynamicContext> OnTS_Repository,
            IGenericRepository<AppDbContextMain> genericRepositoryOnBD)
        {
            _phcMainRepository = Main_Repository;
            _phcDynamicRepository = OnTS_Repository;
            _genericRepositoryOnBD = genericRepositoryOnBD;
        }


        public KhulipaService()
        {

        }

        public Task<RespostaDTO> FilaInserirPagamentos(List<PaymentInsertDTO> paymentDetailsDTO)
        {

            //decimal responseID = logHelper.generateResponseID().Value;
            decimal responseID = 0;
            RespostaDTO respostaDTO = new();

            try
            {

                List<UProvider> providerData = _phcRepository.GetUProvider(pagamento.Canal);
                var serviceProviderCodeData = providerData.Where(providerData => providerData.chave == "serviceProviderCode").FirstOrDefault();

                U2bPayments u2BPayments = new U2bPayments
                {
                    U2bPaymentsstamp = 25.UseThisSizeForStamp(),
                    Oristamp = pagamento.Oristamp,
                    Tabela = pagamento.Tabela,
                    Valor = pagamento.Valor,
                    Moeda = "MZN",
                    Canal = (int)pagamento.Canal,
                    Transactionid = pagamento.Referencia,
                    Origem = pagamento.MSISDN,
                    Destino = serviceProviderCodeData.valor,
                    Tipo = "Carteira Móvel",
                    Estado = "Por Enviar",
                    Descricao = "Pagamento por enviar",

                    Ousrdata = DateTime.Now.Date,
                    Ousrhora = DateTime.Now.ToString("HH:mm"),
                    Ousrinis = "2bPayments",
                };

                Debug.Print($"U2bPayments    {JsonConvert.SerializeObject(u2BPayments)}");

                _genericPHCRepository.Add(u2BPayments);
                _genericPHCRepository.SaveChanges();


                Debug.Print($" PROCESSANDO O PAGAMENTO {pagamento.Referencia}");
                var estadoDoPagamento = await mpesaApi.PaymentQueryProviderRoute(pagamento);
                var response = new ResponseDTO(estadoDoPagamento.response, estadoDoPagamento.Data, pagamento.ToString());


                //var response = await providerRoute.paymentProviderRoute(pagamento);
                //logHelper.generateLogJB(response, pagamento.transactionId, "PaymentService.processarPagamento");
                Debug.Print($"ESTADO DO PAGAMENTO {response}");
                //_iPaymentRespository.actualizarEstadoDoPagamento(pagamento, response);

                switch (estadoDoPagamento.response.cod)
                {
                    case "00077":
                        response = await mpesaApi.C2BPaymentProviderRoute(pagamento);
                        Debug.Print($"RESPOSTA DO PAGAMENTO 00077 {(response.Data as Response)?.Description}");
                        logHelper.generateLogJB_PHC(response, pagamento.Referencia, "PaymentService.processarPagamento");

                        //_iPaymentRespository.actualizarEstadoDoPagamento(pagamento, response);
                        break;


                    case "0000":
                    case "000":
                        Debug.Print($"Pagamento Existente  {(response.Data as Response)?.Description} .");
                        response = new ResponseDTO(new ResponseCodesDTO("0000", "Success"), estadoDoPagamento.Data, pagamento.ToString());
                        logHelper.generateLogJB_PHC(response, pagamento.Referencia, "PaymentService.processarPagamento");

                        //_iPaymentRespository.actualizarEstadoDoPagamento(pagamento, response);
                        break;

                    case "0002":

                        response = await mpesaApi.C2BPaymentProviderRoute(pagamento);
                        Debug.Print($"RESPOSTA DO PAGAMENTO  0002  {response}");
                        logHelper.generateLogJB_PHC(response, pagamento.Referencia, "PaymentService.processarPagamento");

                        //_iPaymentRespository.actualizarEstadoDoPagamento(pagamento, response);
                        break;

                }

                //var response = new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), null, null);

                respostaDTO = GerarResposta((response.Data as Response), responseID);

                u2BPayments.Estado = respostaDTO.Estado;
                u2BPayments.Descricao = respostaDTO.Descricao;
                _genericPHCRepository.SaveChanges();


                return respostaDTO;
            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"GetGLTransactions ERROR {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                //logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "GetGLTransactions", errorDTO?.ToString());
                logHelper.generateLogJB_PHC(finalResponse, logHelper.generateResponseID().ToString(), "GetGLTransactions");

                respostaDTO = new RespostaDTO("", "0007", errorDTO.message);
                return respostaDTO;
            }

        }





    }

}