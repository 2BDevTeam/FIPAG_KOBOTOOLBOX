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
using AutoMapper.Features;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class KOBOService : IKoboService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboAPI koboAPI = new KoboAPI();


        //private readonly IPHCRepository _PHCRespository;
        private readonly IPHCMainRepository<AppDbContextMain> _phcMainRepository;
        private readonly IPHCRepository2<DynamicContext> _phcDynamicRepository;
        private readonly IGenericRepository<AppDbContextMain> _genericRepositoryOnBD;

        public KOBOService(IPHCMainRepository<AppDbContextMain> Main_Repository, IPHCRepository2<DynamicContext> OnTS_Repository,
            IGenericRepository<AppDbContextMain> genericRepositoryOnBD)
        {
            _phcMainRepository = Main_Repository;
            _phcDynamicRepository = OnTS_Repository;
            _genericRepositoryOnBD = genericRepositoryOnBD;
        }


        public KOBOService()
        {

        }


        public async Task SyncClientesOBA()
        {

            try
            {
                var clsOBA = _phcMainRepository.GetOBAClientes();
                Debug.Print($"Clientes OBA  {clsOBA.Count}");

                foreach (var oBA in clsOBA)
                {
                    var cl = _phcMainRepository.GetClByNo((int)oBA.PhcId);

                    if (cl == null)
                    {
                        Debug.Print($"Cliente com PhcId {oBA.PhcId} não encontrado.");

                        oBA.Error = "Não encontrado no PHC";
                        continue;
                    }

                    var cl2 = _phcMainRepository.GetCl2ByStamp(cl.Clstamp);
                    cl2.UKoboSync = true;
                    cl2.UKoboOri = true;
                    cl2.UKoboid = (decimal)oBA.KoboId;


                    cl.Nome2 = cl.Nome2;

                    _genericRepositoryOnBD.SaveChanges();

                    oBA.Sync = true;
                }

                _genericRepositoryOnBD.BulkUpdate(clsOBA);
                _genericRepositoryOnBD.SaveChanges();

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"ProcessarFormularios ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "ProcessarFormularios", errorDTO?.ToString());
            }
        }


        public async Task SincronizarFt()
        {

            var consumos = _phcMainRepository.GetConsumos();
            Debug.Print($"TOTAL DE FATURAS POR SINCRONIZAR {consumos.Count()}");

            foreach (var consumo in consumos)
            {
                //Debug.Print("fttttttttt " + consumo.Ftstamp);
                //BackgroundJob.Enqueue(() => SyncFactura(consumo));

            }

        }



        public async Task ProcessarFormularios(string nomeBd)
        {

            try
            {
                var bd = _phcMainRepository.GetBaseDados(nomeBd);

                if (bd == null)
                {
                    throw new Exception($"Base de Dados {nomeBd} não encontrada.");
                }

                Debug.Print($"BDs    {bd.Nomebd}");

                var formularios = _phcMainRepository.GetLiBaseDados(bd.UBasedadosstamp);

                var connectionString = $"Server={bd.Nomesrv.Trim()};Database={bd.Nomebd.Trim()};User Id={bd.Username.Trim()};Password={bd.Password.Trim()};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False";
                Debug.Print($"Connectiosn   {connectionString}");


                var dynamicContext = new DynamicContext(connectionString);



                var aux = _phcDynamicRepository.GetBoTeste(dynamicContext);


                foreach (var formulario in formularios)
                {
                    SincrinizarDadosUSyncQueue(formulario, dynamicContext);

                }

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"ProcessarFormularios ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "ProcessarFormularios", errorDTO?.ToString());
            }

        }


        public async Task SincrinizarDadosUSyncQueue(ULibasedado formulario, DynamicContext dynamicContext)
        {
            Debug.Print($"SincrinizarDadosUSyncQueue    {formulario.SubNome}");

            switch (formulario.SubNome)
            {

                case "Consumo":
                    var syncQueueFt = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "ft");
                    Debug.Print($"TOTAL DE Consumos DA BD {formulario.Basedadosstamp} POR SINCRONIZAR {syncQueueFt.Count()}");

                    foreach (var sq in syncQueueFt)
                    {
                        ProcessFatura(sq, sq.Stamptabela, sq.Accao, formulario.Formid, dynamicContext);
                    }
                    _phcDynamicRepository.SaveChanges(dynamicContext);

                    break;

                case "Ligação":

                    var syncQueueCl = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "cl");
                    Debug.Print($"TOTAL DE Ligações DA BD {formulario.Basedadosstamp} POR SINCRONIZAR {syncQueueCl.Count()}");

                    foreach (var sq in syncQueueCl)
                    {
                        ProcessCliente(sq, sq.Stamptabela, sq.Accao, sq.campo, sq.valor, formulario.Basedadosstamp, dynamicContext);

                    }
                    _phcDynamicRepository.SaveChanges(dynamicContext);

                    break;

                case "Levantamento":

                    AdicionarLevantamentoBeneficiarios(formulario.Formid, dynamicContext);

                    break;

                default: break;

            }

        }



        public void AdicionarLevantamentoBeneficiarios(string formID, DynamicContext dynamicContext)
        {

            try
            {
                var dados = koboAPI.GetFormNaoAdicionadosPHC(formID);


                if (dados == null)
                {
                    throw new Exception($"Null em {formID}");
                }

                if (dados.results.Count == 0)
                {
                    throw new Exception($"Sem dados em {formID}");
                }

                Debug.Print($"Beneficiarios nao adicionados PHC {dados.results.Count}");

                var emNo = _phcDynamicRepository.GetNoEm(dynamicContext);

                foreach (var dado in dados.results)
                {
                    Debug.Print($"Benefeeeeeee {dado._id}");

                    var upd = koboAPI.UpdNaoAdicionadosPHC(dado._id, formID).results;

                    var em = new Em
                    {
                        Emstamp = 25.UseThisSizeForStamp(),
                        No = emNo,
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

                    _phcDynamicRepository.Add(dynamicContext, em);
                    emNo++;
                }

                _phcDynamicRepository.SaveChanges(dynamicContext);

            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO AdicionarLevantamentoBeneficiarios {ex.Message}");
            }

        }


        public void ProcessCliente(USyncQueue usync, string stamp, string accao, string campo, string valor, string bdStamp, DynamicContext dynamicContext)
        {

            switch (accao)
            {
                case "UPDATE":
                    ProcessClienteUpd(usync, stamp, campo, valor, bdStamp, dynamicContext);
                    break;

                default:
                    break;
            }
        }

        public void ProcessFatura(USyncQueue usync, string ftstamp, string accao, string formID, DynamicContext dynamicContext)
        {

            switch (accao)
            {
                case "INSERT":
                    var consumo = _phcDynamicRepository.GetConsumo(dynamicContext, ftstamp);
                    SyncFactura(usync, consumo, formID, dynamicContext);
                    break;

                default:
                    break;
            }
        }



        public void ProcessClienteUpd(USyncQueue usync, string stamp, string campo, string valor, string bdStamp, DynamicContext dynamicContext)
        {
            Debug.Print($"ProcessClienteUpd ");
            //var cl2 = _phcDynamicRepository.GetCl2PorStamp(dynamicContext, stamp);

            switch (campo)
            {
                case "tipo":
                    var li = _phcDynamicRepository.GetClNaoSincronizadosLigacoes(dynamicContext, stamp);

                    if (valor == "1-Activo")
                    {
                        SyncLigacao(usync, li, valor, bdStamp, dynamicContext);
                    }
                    else if (valor == "2-Suspenso" || valor == "3-Rescindido")
                    {
                        li.dataLigacao = li.dataTermino;
                        SyncLigacao(usync, li, valor, bdStamp, dynamicContext);
                    }

                    break;

                default:
                    break;
            }
        }



        public void SyncFactura(USyncQueue usync, Consumos cons, string formID, DynamicContext dynamicContext)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset localTime = now.ToOffset(TimeSpan.FromHours(2));
            string formattedDate = localTime.ToString("yyyy-MM-dd HH:mm:ss");

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
                        start = formattedDate,
                        end = formattedDate,
                        today = formattedDate,
                        username = "PHC",

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

                var insertFt = koboAPI.AddDataToKobo(body, formID);

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    throw new Exception("Erro a introduzir a fatura no KoboToolbox.");
                }

                var ft2 = _phcDynamicRepository.GetFt2(dynamicContext, cons.Ftstamp);
                ft2.UKobosync = true;

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);

                _phcDynamicRepository.SaveChanges(dynamicContext);

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncFactura ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncFactura", errorDTO?.ToString());
            }
        }


        public void SyncLigacao(USyncQueue usync, Ligacoes lig, string estado, string stampBD, DynamicContext dynamicContext)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset localTime = now.ToOffset(TimeSpan.FromHours(2));
            string formattedDate = localTime.ToString("yyyy-MM-dd HH:mm:ss");

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
                        start = formattedDate,
                        end = formattedDate,
                        today = formattedDate,
                        username = "PHC",

                        Group1Ligacoes = new Group1Ligacoes
                        {
                            no = (int)lig.No,
                            nome = lig.Nome,
                            DataLigacao = lig.dataLigacao.ToString("yyyy-MM-dd"),
                            IDBenefKobo = lig.IDBenefKobo,
                            EstadoLigacao = estado
                        }
                    }
                };

                string json = JsonConvert.SerializeObject(body);

                JObject jsonObject = JObject.Parse(json);
                koboAPI.RemoveNullProperties(jsonObject);


                string cleanedJson = jsonObject.ToString(Formatting.None);
                Debug.Print($"Json limpo Fatura {cleanedJson}");

                var formID = _phcMainRepository.GetFormID("Ligação", stampBD);
                Debug.Print($"stampBD {stampBD}");

                var insertFt = koboAPI.AddDataToKobo(body, formID.Formid);

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    throw new Exception("Erro a introduzir a Ligação no KoboToolbox.");
                }

                formID = _phcMainRepository.GetFormID("Levantamento", stampBD);
                var updBeneficiario = koboAPI.UpdIsClientePHC(lig.IDBenefKobo, formID.Formid);

                var cliente = _phcDynamicRepository.GetCl2PorIdKobo(dynamicContext, lig.IDBenefKobo);

                cliente.UKoboSync = true;

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);

                _phcDynamicRepository.SaveChanges(dynamicContext);
            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncFactura ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncFactura", errorDTO?.ToString());
            }
        }

        public void SyncLigacaoUpdate(Ligacoes lig)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            DateTimeOffset localTime = now.ToOffset(TimeSpan.FromHours(2));
            string formattedDate = localTime.ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                //var updBeneficiario = koboAPI.UpdEstadoLigacao(lig.IDBenefKobo);

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


        /*
        public ResponseDTO RegistarCliente(int id, string DB)
        {

            var cliente = Sw_GetCl2IdKobo(id, DB);

            var responseID = logHelper.generateResponseID();

            var formID = Sw_GetUFormId("Levantamento de", DB);
            var response = koboAPI.UpdIsClientePHC(id, formID.Formid);

            //try

            if (response.failures == 0)
            {
                cliente.UKoboSync = true;
                cliente.UOrigem = "KoboToolbox";
                Sw_SaveChanges(DB);
            }


            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);
        }
        */



    }

}
