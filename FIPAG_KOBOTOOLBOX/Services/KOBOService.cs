﻿using AutoMapper;
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
using FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs;
using AutoMapper.Features;

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class KOBOService : IKoboService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboAPI koboAPI = new KoboAPI();


        private readonly IPHCRepository _KOBORespository;
        private readonly IGenericRepository _genericRepository;
        private readonly AppDbContext _appDbContext;

        public KOBOService(IPHCRepository KOBORepository, IGenericRepository genericRepository, AppDbContext appDbContext)
        {
            _KOBORespository = KOBORepository;
            _appDbContext = appDbContext;
            _genericRepository = genericRepository;
        }

        public KOBOService()
        {

        }

        public async Task AdicionarLevantamentoBeneficiarios()
        {
            //var dados = koboAPI.GetResult("Benjamim Vembane").results;

            try
            {
                var dados = koboAPI.GetFormNaoAdicionadosPHC();
                if (dados == null)
                {

                    throw new Exception("Dados do Kobo retornaram Null");
                }

                Debug.Print($"Beneficiarios nao adicionados PHC {dados.results.Count}");

                foreach (var dado in dados.results)
                {
                    Debug.Print($"Benefeeeeeee {dado._id}");
                    var upd = koboAPI.UpdNaoAdicionadosPHC(dado._id).results;

                    var em = new Em
                    {
                        Emstamp = 25.UseThisSizeForStamp(),
                        No = _KOBORespository.GetNoEm(),
                        Zona = dado.Bairro,
                        Nome = dado.nome_chefe_af.Trim(),
                        Pais = dado.PaisOrigem,
                        UNascimen = dado.DataDeNascimento,
                        Local = dado.cidade,
                        Morada = $"{dado.endereco}, {dado.Quarteirao}, {dado.ncasa}",
                        UBidata = dado.DataEmissaoBI,
                        UBino = dado.NrBi,
                        UBilocal = dado.LocalEmissaoBI,
                        Telefone = dado.telefone,
                        Ncont = dado.nuit,
                        Pncont = "MZ",
                        Ousrinis = dado.nuit,
                        UOrigem = "KoboToolbox",
                        //UNquart =dado.Quarteirao,
                        UNcasa = dado.ncasa,
                        UEndereco = dado.endereco,
                        UKoboid = dado._id,
                        UKoboori = true,
                        Ousrdata = DateTime.Now.Date,
                        Usrdata = DateTime.Now.Date,
                        Ousrhora = DateTime.Now.ToString("HH:mm"),
                        Usrhora = DateTime.Now.ToString("HH:mm")
                    };


                    _genericRepository.Add(em);
                    _genericRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO AdicionarLevantamentoBeneficiarios {ex.Message}");
            }


        }


        public async Task AdicionarLigacoesDeCls()
        {
            var ligacoes = _KOBORespository.GetClNaoSincronizadosLigacoes();
            Debug.Print($"TOTAL DE CLIENTES LICAÇÃO POR SINCRONIZAR {ligacoes.Count()}");

            foreach (var li in ligacoes)
            {
                //RegistarCliente(li.IDBenefKobo);
                SyncLigacao(li);
            }
        }


        public async Task SincronizarFt()
        {

            var consumos = _KOBORespository.GetConsumos();
            Debug.Print($"TOTAL DE FATURAS POR SINCRONIZAR {consumos.Count()}");

            foreach (var consumo in consumos)
            {
                Debug.Print("fttttttttt " + consumo.Ftstamp);
                BackgroundJob.Enqueue(() => SyncFactura(consumo));

            }

        }

        public void SyncFactura(Consumos cons)
        {

            try
            {

                var data = cons.Fdata.ToString("yyyy-MM-dd") + "T00:00:00+02:00";

                InsertFormDTO body = new()
                {
                    id = "",
                    submission = new SubmissionDataDTO
                    {
                        meta = new MetaDataDTO
                        {
                            instanceID = "uuid:2BPHCadmin"
                        },
                        Group1Consumos = new Group1Consumos
                        {
                            no = cons.No,
                            nome = cons.Nome.Trim(),
                            IDBenefKobo = cons.IDBenefKobo
                        },
                        Group2Consumos = new Group2Consumos
                        {
                            nmdoc = cons.Nmdoc,
                            fno = cons.Fno,
                            totalMeticais = cons.TotalMeticais,
                            fdata = data
                        },
                        Group3Consumos = new Group3Consumos
                        {
                            tipoFatura = cons.TipoFatura,
                            periodo = cons.Periodo,
                            leituraAnterior = cons.LeituraAnterior,
                            leituraActual = cons.LeituraActual,
                            consumoFaturado = cons.ConsumoFaturado,
                            consumoReal = cons.ConsumoReal,
                            anomalia = cons.Anomalia
                        }
                    }
                };

                string json = JsonConvert.SerializeObject(body);

                JObject jsonObject = JObject.Parse(json);
                koboAPI.RemoveNullProperties(jsonObject);


                string cleanedJson = jsonObject.ToString(Formatting.None);
                Debug.Print($"Json limpo Fatura {cleanedJson}");

                var insertFt = koboAPI.AddDataToKobo(body, "Consumos");

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    throw new Exception("Erro a introduzir a fatura no KoboToolbox.");
                }

                var ft = _KOBORespository.GetFt(cons.Ftstamp);

                ft.UKobosync = true;

                _genericRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncFactura ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncFactura", errorDTO?.ToString());
            }
        }


        public void SyncLigacao(Ligacoes lig)
        {

            try
            {

                InsertFormDTO body = new()
                {
                    id = "",
                    submission = new SubmissionDataDTO
                    {
                        meta = new MetaDataDTO
                        {
                            instanceID = "uuid:2BPHCadmin"
                        },
                        Group1Ligacoes = new Group1Ligacoes
                        {
                            no = (int)lig.No,
                            nome = lig.Nome,
                            DataLigacao = lig.dataLigacao.ToString("yyyy-MM-dd") + "T00:00:00+02:00",
                            IDBenefKobo = lig.IDBenefKobo
                        }
                    }
                };

                string json = JsonConvert.SerializeObject(body);

                JObject jsonObject = JObject.Parse(json);
                koboAPI.RemoveNullProperties(jsonObject);


                string cleanedJson = jsonObject.ToString(Formatting.None);
                Debug.Print($"Json limpo Fatura {cleanedJson}");

                var insertFt = koboAPI.AddDataToKobo(body, "Ligações");

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    throw new Exception("Erro a introduzir a Ligação no KoboToolbox.");
                }

                var updBeneficiario = koboAPI.UpdIsClientePHC(lig.IDBenefKobo);


                var cliente = _KOBORespository.GetClPorIdKobo(lig.IDBenefKobo);
                cliente.UKoboSync = true;
                
                _genericRepository.SaveChanges();

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncFactura ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncFactura", errorDTO?.ToString());
            }
        }



        public ResponseDTO GetResult(string nome)
        {
            var responseID = logHelper.generateResponseID();
            var response = koboAPI.GetResult(nome);

            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);
        }

        public ResponseDTO RegistarCliente(int id)
        {
            var cliente = _KOBORespository.GetClPorIdKobo(id);

            var responseID = logHelper.generateResponseID();
            //var response2 = koboAPI.UpdIsClientePHC(id);
            var response = koboAPI.UpdIsClientePHC(id);

            //try


            if (response.failures == 0)
            {
                cliente.UKoboSync = true;
                cliente.UOrigem = "KoboToolbox";
                _genericRepository.SaveChanges();
            }


            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);
        }
    }

}
