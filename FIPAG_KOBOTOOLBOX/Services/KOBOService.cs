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
using FIPAG_KOBOTOOLBOX.Persistence.APIs.DTOs;

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class KOBOService : IKOBOService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboToolBoxAPI koboAPI = new KoboToolBoxAPI();


        private readonly IKOBORepository _KOBORespository;
        private readonly IGenericRepository _genericRepository;
        private readonly AppDbContext _appDbContext;

        public KOBOService(IKOBORepository KOBORepository, IGenericRepository genericRepository, AppDbContext appDbContext)
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
            var dados = koboAPI.GetFormNaoAdicionadosPHC().results;

            Debug.Print($"Beneficiarios nao adicionados PHC {dados.Count}");

            foreach (var dado in dados)
            {
                Debug.Print($"Benefeeeeeee {dado._id}");
                try
                {
                    var upd = koboAPI.UpdNaoAdicionadosPHC(dado._id).results;

                    var em = new Em
                    {
                        Emstamp = 25.UseThisSizeForStamp(),
                        No = GetNoEm(),
                        Nome = dado.nome_chefe_af,
                        Pais = dado.PaisOrigem,
                        UNascimen = dado.DataDeNascimento,

                        UBiNo = dado.NrBi,
                        UBiLocal = dado.LocalEmissaoBI,
                        Telefone = dado.telefone,
                        Ncont = dado.nuit,
                        //UNquart =dado.Quarteirao,
                        UNcasa = dado.ncasa,
                        UEndereco = dado.endereco,
                        UKoboId = dado._id,
                        Ousrdata = DateTime.Now.Date,
                        Usrdata = DateTime.Now.Date,
                        Ousrhora = DateTime.Now.ToString("HH:mm"),
                        Usrhora = DateTime.Now.ToString("HH:mm")
                    };


                    _genericRepository.Add(em);
                    _genericRepository.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.Print($"ERRO AO AdicionarLevantamentoBeneficiarios {ex.Message}");
                }
            }


        }

        public decimal GetNoEm()
        {
            return _appDbContext.Em
                         .Select(em => em.No)
                         .ToList()
                         .DefaultIfEmpty(0)
                         .Max() + 1;
        }



        public ResponseDTO GetResult(string nome)
        {
            var responseID = logHelper.generateResponseID();
            var response = koboAPI.GetResult(nome);

            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);

        }

        public ResponseDTO RegistarCliente(int id)
        {
            var responseID = logHelper.generateResponseID();
            var response = koboAPI.UpdIsClientePHC(id);

            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);

        }
    }

}
