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


        /* SyncFactura Mas esqueci em que contexto utilizei esse método
        public void SyncFactura(Consumos cons, string formId)
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

                var insertFt = koboAPI.AddDataToKobo(body, formId);

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    throw new Exception("Erro a introduzir a fatura no KoboToolbox.");
                }

                var ft2 = _phcMainRepository.GetFt2(cons.Ftstamp);

                ft2.UKobosync = true;

                _genericRepositoryOnBD.SaveChanges();

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncFactura ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncFactura", errorDTO?.ToString());
            }
        }
        */


        public bool VerificarJobActivos(DynamicContext dynamicContext, string lockKey)
        {
            // Obter o job lock baseado no lockKey
            var jobLock = _phcDynamicRepository.GetJobLocks(dynamicContext, lockKey);

            // Verificar se o jobLock existe
            if (jobLock != null)
            {
                // Verificar se o job está em execução
                if (jobLock.IsRunning)
                {
                    // Verificar se o job está a ser executado por mais de 1 hora
                    DateTime oneHourAgo = DateTime.Now.AddHours(-1);
                    if (jobLock.DataExec < oneHourAgo)
                    {
                        // Atualizar a data e hora para o tempo atual
                        jobLock.DataExec = DateTime.Now;
                        //_phcDynamicRepository.Update(dynamicContext, jobLock);
                        _phcDynamicRepository.SaveChanges(dynamicContext);

                        Debug.Print("O job estava em execução há mais de 1 hora. A data foi atualizada.");
                    }
                    else
                    {
                        Debug.Print("O job já está em execução.");
                        return true;
                    }
                }
            }
            else
            {
                // Caso não haja um jobLock, criar um novo
                DateTime now = DateTime.Now;
                jobLock = new JobLocks { JobId = lockKey, IsRunning = true, DataExec = now };
                _phcDynamicRepository.Add(dynamicContext, jobLock);
                _phcDynamicRepository.SaveChanges(dynamicContext);
            }

            return false;
        }


        public void TerminarJob(DynamicContext dynamicContext, string lockKey)
        {
            var jobLock = _phcDynamicRepository.GetJobLocks(dynamicContext, lockKey);

            _phcDynamicRepository.Delete(dynamicContext, jobLock);
            _phcDynamicRepository.SaveChanges(dynamicContext);
        }


        [DisableConcurrentExecution(timeoutInSeconds: 40 * 60)]
        public async Task ProcessarFormularios(string nomeBd, string cidade)
        {

            var bd = _phcMainRepository.GetBaseDados(nomeBd) ?? throw new Exception($"Base de Dados {nomeBd} não encontrada.");

            Debug.Print($"BDs    {bd.Nomebd}");

            var formularios = _phcMainRepository.GetLiBaseDados(bd.UBasedadosstamp);

            var connectionString = $"Server={bd.Nomesrv.Trim()};Database={bd.Nomebd.Trim()};User Id={bd.Username.Trim()};Password={bd.Password.Trim()};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False";
            Debug.Print($"Connectiosn   {connectionString}");


            var dynamicContext = new DynamicContext(connectionString);

            string lockKey = "ProcessarFormularios";

            if (VerificarJobActivos(dynamicContext, lockKey))
                return;

            try
            {
                foreach (var formulario in formularios)
                {
                    SincrinizarDadosUSyncQueue(formulario, cidade, dynamicContext);
                }

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"ProcessarFormularios ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "ProcessarFormularios", errorDTO?.ToString());
            }
            finally
            {
                TerminarJob(dynamicContext, lockKey);
            }

            //TerminarJob(dynamicContext, lockKey);

        }


        public async Task SincrinizarDadosUSyncQueue(ULibasedado formulario, string cidade, DynamicContext dynamicContext)
        {
            Debug.Print($"SincronizarDadosUSyncQueue   {formulario.Subnome} - {formulario.FormCidade}");

            switch (formulario.Subnome)
            {
                case "Ligação":

                    var syncQueueCl = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "cl", "tipo");
                    Debug.Print($"TOTAL DE Ligações DA BD {formulario.Basedadosstamp} POR SINCRONIZAR {syncQueueCl.Count()}");

                    foreach (var sq in syncQueueCl)
                    {
                        ProcessSyncDados(sq, "cl", sq.Stamptabela, sq.Accao, sq.campo, sq.valor, formulario, dynamicContext);
                    }
                    _phcDynamicRepository.SaveChanges(dynamicContext);

                    break;

                case "Levantamento":
                    if (formulario.FormCidade == cidade)
                    {
                        AdicionarLevantamentoBeneficiarios(formulario.Formid, cidade, dynamicContext);

                        var syncQueueEm = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "em", "no");
                        foreach (var sq in syncQueueEm)
                        {
                            ProcessSyncDados(sq, "em", sq.Stamptabela, sq.Accao, sq.campo, sq.valor, formulario, dynamicContext);
                        }
                        var syncQueueCl1 = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "cl", "no");
                        foreach (var sq in syncQueueCl1)
                        {
                            ProcessSyncDados(sq, "cl", sq.Stamptabela, sq.Accao, sq.campo, sq.valor, formulario, dynamicContext);
                        }

                        _phcDynamicRepository.SaveChanges(dynamicContext);

                    }

                    break;

                case "Consumo":
                    var syncQueueFt = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "ft");
                    Debug.Print($"TOTAL DE Consumos DA BD {formulario.Basedadosstamp} POR SINCRONIZAR {syncQueueFt.Count()}");

                    foreach (var sq in syncQueueFt)
                    {
                        ProcessFatura(sq, sq.Stamptabela, sq.Accao, formulario.Formid, dynamicContext);
                    }
                    _phcDynamicRepository.SaveChanges(dynamicContext);

                    var syncQueueFt1 = _phcDynamicRepository.GetUSyncQueue(dynamicContext, "ft", "anulado");
                    foreach (var sq in syncQueueFt1)
                    {
                        ProcessSyncDados(sq, "ft", sq.Stamptabela, sq.Accao, sq.campo, sq.valor, formulario, dynamicContext);
                    }
                    _phcDynamicRepository.SaveChanges(dynamicContext);

                    break;

                default: break;

            }

        }



        public void AdicionarLevantamentoBeneficiarios(string formID, string cidade, DynamicContext dynamicContext)
        {

            try
            {
                var dados = koboAPI.GetFormNaoAdicionadosPHC(formID, cidade) ?? throw new Exception($"Null em {formID}");

                if (dados.results.Count == 0)
                {
                    throw new Exception($"Sem dados em {formID}");
                }

                Debug.Print($"Beneficiarios nao adicionados PHC {dados.results.Count}");


                var emNo = _phcDynamicRepository.GetNoEm(dynamicContext);

                foreach (var dado in dados.results)
                {
                    Debug.Print($"Benefeeeeeee {dado._id}");


                    var localiz = new string[] { "0", "0", "0", "0" };

                    if (dado.localizacao != null)
                        localiz = dado.localizacao.Split(' ');

                    if (dado.localizacao2 != null)
                        localiz = dado.localizacao2.Split(' ');

                    var lBairro = dado.Bairro.Substring(0, Math.Min(dado.Bairro.Length, 12));

                    var quarteirao = dado.Quarteirao;
                    var ncasa = dado.ncasa;


                    List<string> partesMorada = new List<string>();

                    if (!string.IsNullOrWhiteSpace(lBairro))
                    {
                        partesMorada.Add(lBairro);
                    }

                    if (!string.IsNullOrWhiteSpace(quarteirao) && quarteirao != "0")
                    {
                        partesMorada.Add(quarteirao);
                    }

                    if (ncasa != 0)
                    {
                        partesMorada.Add(ncasa.ToString());
                    }

                    string morada = string.Join(", ", partesMorada);

                    Debug.Print($"Benefeeeeeee {dado.localizacao}");
                    var em = new Em
                    {
                        Emstamp = 25.UseThisSizeForStamp(),
                        No = emNo,
                        Zona = lBairro,
                        Nome = dado.nome_chefe_af.Trim(),
                        Pais = dado.PaisOrigem,
                        UNascimen = dado.DataDeNascimento,
                        Local = dado.cidade,
                        Morada = morada,
                        UBidata = dado.DataEmissaoBI,
                        UBino = dado.NrBi,
                        UBilocal = dado.LocalEmissaoBI,
                        Tlmvl = dado.telefone,
                        Ncont = dado.nuit,
                        Segmento = "Doméstico",
                        Pncont = "MZ",
                        Ousrinis = "FIKOBOWS",
                        UOrigem = "KoboToolbox",
                        //UNquart =dado.Quarteirao,
                        UNcasa = dado.ncasa,
                        UEndereco = dado.endereco,
                        UKoboid = dado._id,
                        UKoboori = true,
                        Latitude = ParseDecimal(localiz[0]),
                        Longitude = ParseDecimal(localiz[1]),
                        Ousrdata = DateTime.Now.Date,
                        Usrdata = DateTime.Now.Date,
                        Ousrhora = DateTime.Now.ToString("HH:mm"),
                        Usrhora = DateTime.Now.ToString("HH:mm")
                    };
                    Debug.Print($"dado.id {em.UEndereco}");

                    //var upd = koboAPI.UpdNaoAdicionadosPHC(dado._id, formID).results;
                    var upd = koboAPI.UpdNaoAdicionadosPHC(dado._id, formID);

                    if (upd == null)
                        throw new Exception("Erro ao actualizar dados");

                    if (upd.successes != 1)
                        throw new Exception("Erro ao actualizar dados");

                    _phcDynamicRepository.Add(dynamicContext, em);
                    _phcDynamicRepository.SaveChanges(dynamicContext);


                    emNo++;
                }

            }
            catch (DbUpdateException ex)
            {
                Debug.Print("An error occurred while updating the database.");
                Debug.Print($"Error: {ex.InnerException?.Message ?? ex.Message}");
                throw; // Rethrow the exception to preserve stack trace if needed
            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO AdicionarLevantamentoBeneficiarios {ex.Message}");
            }


        }


        public void ActualizarDadosEm(USyncQueue usync, ULibasedado formulario, string no, DynamicContext dynamicContext)
        {

            try
            {
                int emno = int.Parse(no);
                var em = _phcDynamicRepository.GetEm(dynamicContext, emno);

                UpdateFormDTO body = new()
                {
                    payload = new PayloadFormDTO
                    {
                        submission_ids = new List<long> { (long)em.UKoboid },
                        data = new DataFormDTO
                        {
                            nome_chefe_af = em.Nome,
                            DataDeNascimento = em.UNascimen,
                            DataEmissaoBI = em.UBidata,
                            NrBi = em.UBino,
                            LocalEmissaoBI = em.UBilocal,
                            telefone = em.Tlmvl,
                            nuit = em.Ncont,
                            ncasa = em.UNcasa.ToString()
                        }
                    },
                };

                var upd = koboAPI.UpdDataForm(body, formulario.Formid) ?? throw new Exception("Erro ao actualizar dados");

                if (upd.successes != 1)
                    throw new Exception("Erro ao actualizar dados");

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);

                _phcDynamicRepository.SaveChanges(dynamicContext);
            }
            catch (DbUpdateException ex)
            {
                Debug.Print("An error occurred while updating the database.");
                Debug.Print($"Error: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO AdicionarLevantamentoBeneficiarios {ex.Message}");
            }

        }


        public void ActualizarDadosFt(USyncQueue usync, string stamp, string campo, string valor, ULibasedado formulario, DynamicContext dynamicContext)
        {
            try
            {
                var ft = _phcDynamicRepository.GetFt(dynamicContext, stamp);
                var cl = _phcDynamicRepository.GetCliente(dynamicContext, (int)ft.No);

                UpdateFormDTO body = new()
                {
                    payload = new PayloadFormDTO
                    {
                        submission_ids = new List<long> { (long)cl.cl2.UKoboid },
                        data = new DataFormDTO
                        {
                            anulada = ft.Anulado ? "true" : "false",
                        }
                    },
                };

                var upd = koboAPI.UpdDataForm(body, formulario.Formid) ?? throw new Exception("Erro ao actualizar dados");

                if (upd.successes != 1)
                    throw new Exception("Erro ao actualizar dados");

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);

                _phcDynamicRepository.SaveChanges(dynamicContext);
            }
            catch (DbUpdateException ex)
            {
                Debug.Print($"Error: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO ActualizarDadosFt {ex.Message}");
            }

        }

        public void ActualizarDadosCl(USyncQueue usync, ULibasedado formulario, string stamp, DynamicContext dynamicContext)
        {

            try
            {
                var cliente = _phcDynamicRepository.GetCliente(dynamicContext, stamp);

                UpdateFormDTO body = new()
                {
                    payload = new PayloadFormDTO
                    {
                        submission_ids = new List<long> { (long)cliente.cl2.UKoboid },
                        data = new DataFormDTO
                        {
                            nome_chefe_af = cliente.cl.Nome,
                            DataDeNascimento = cliente.cl.Nascimento,
                            DataEmissaoBI = cliente.cl.Bidata,
                            NrBi = cliente.cl.Bino,
                            LocalEmissaoBI = cliente.cl.Bilocal,
                            telefone = cliente.cl.Tlmvl,
                            nuit = cliente.cl.Ncont,
                            //ncasa = (int)cliente.cl.UNcasa

                        }
                    },
                };

                var upd = koboAPI.UpdDataForm(body, formulario.Formid) ?? throw new Exception("Erro ao actualizar dados");

                if (upd.successes != 1)
                    throw new Exception("Erro ao actualizar dados");

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);

                _phcDynamicRepository.SaveChanges(dynamicContext);
            }
            catch (DbUpdateException ex)
            {
                Debug.Print($"Error: {ex.InnerException?.Message ?? ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO ActualizarDadosCl {ex.Message}");
            }

        }

        private decimal ParseDecimal(string value)
        {
            if (Decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }
            else
            {
                Debug.Print($"Unable to parse '{value}' as a decimal.");
                throw new FormatException($"Unable to parse '{value}' as a decimal.");
            }
        }

        public void ProcessSyncDados(USyncQueue usync, string tabela, string stamp, string accao, string campo, string valor, ULibasedado formulario, DynamicContext dynamicContext)
        {

            switch (accao)
            {
                case "UPDATE":

                    switch (tabela)
                    {
                        case "cl":
                            ProcessClienteUpd(usync, stamp, campo, valor, formulario, dynamicContext);
                            break;
                        case "em":
                            ActualizarDadosEm(usync, formulario, valor, dynamicContext);
                            break;
                        case "ft":
                            ActualizarDadosFt(usync, stamp, campo, valor, formulario, dynamicContext);
                            break;
                        default: break;
                    }
                    break;

                default: break;
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



        public void ProcessClienteUpd(USyncQueue usync, string stamp, string campo, string valor, ULibasedado formulario, DynamicContext dynamicContext)
        {
            Debug.Print($"ProcessClienteUpd ");
            //var cl2 = _phcDynamicRepository.GetCl2PorStamp(dynamicContext, stamp);
            try
            {
                switch (campo)
                {
                    case "tipo":
                        var li = _phcDynamicRepository.GetClNaoSincronizadosLigacoes(dynamicContext, stamp);

                        if (valor == "1-Activo")
                        {
                            SyncLigacao(usync, li, valor, formulario, dynamicContext);
                        }
                        else if (valor == "2-Suspenso" || valor == "3-Rescindido")
                        {
                            li.dataLigacao = li.dataTermino;
                            SyncLigacao(usync, li, valor, formulario, dynamicContext);
                        }
                        break;

                    case "no":
                        ActualizarDadosCl(usync, formulario, stamp, dynamicContext);

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"An error occurred: {ex.Message}");
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

                var nome = cons.Nome.Trim().Replace("\u0002", string.Empty);

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
                            nome = nome,
                            IDBenefKobo = cons.IDBenefKobo
                        },
                        Group2Consumos = new Group2Consumos
                        {
                            nmdoc = cons.Nmdoc,
                            fno = cons.Fno,
                            totalMeticais = cons.TotalMeticais,
                            fdata = data,
                            anulado = "false"
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


        public void SyncLigacao(USyncQueue usync, Ligacoes lig, string estado, ULibasedado formulario, DynamicContext dynamicContext)
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


                var updBeneficiarioIsCl = koboAPI.UpdIsClientePHC(lig.IDBenefKobo, formulario.Formid);
                //var updBeneficiarioAdicionado = koboAPI.UpdNaoAdicionadosPHC(lig.IDBenefKobo, formID.Formid);

                var cliente = _phcDynamicRepository.GetCl2PorIdKobo(dynamicContext, lig.IDBenefKobo);

                cliente.UKoboSync = true;




                var formID = _phcMainRepository.GetFormID("Ligação", formulario.Basedadosstamp);
                Debug.Print($"stampBD {formulario.Basedadosstamp}");

                var insertFt = koboAPI.AddDataToKobo(body, formID.Formid);

                if (insertFt == null || insertFt.message != "Successful submission.")
                {
                    AddChangesSyncReport(lig.IDBenefKobo, lig.No, "Erro a introduzir a Ligação no KoboToolbox.", dynamicContext, usync);
                    throw new Exception("Erro a introduzir a Ligação no KoboToolbox.");
                }

                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);
                _phcDynamicRepository.SaveChanges(dynamicContext);

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"SyncLigação ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "SyncLigação", errorDTO?.ToString());
            }
        }


        public void AddChangesSyncReport(long koboId, long no, string motivo, DynamicContext dynamicContext, USyncQueue usync)
        {
            try
            {
                var currentDate = DateTime.Now;
                var liftUntil = currentDate.AddDays(3650);

                Debug.Print($"Erro Add Ligações PARA O CL {no}");

                var syncReport = new USyncreport
                {
                    USyncreportstamp = 25.UseThisSizeForStamp(),
                    Status = "Erro",
                    Obs = motivo,
                    Tabno = no,
                    Koboid = koboId,
                    Ousrdata = DateTime.Now.Date,
                    Usrdata = DateTime.Now.Date,
                    Ousrhora = DateTime.Now.ToString("HH:mm"),
                    Usrhora = DateTime.Now.ToString("HH:mm"),
                };

                _phcDynamicRepository.Add(dynamicContext, syncReport);
                _phcDynamicRepository.DeleteSyncQueue(dynamicContext, usync);
                _phcDynamicRepository.SaveChanges(dynamicContext);

            }
            catch (Exception ex)
            {
                Debug.Print($"ERRO AO PROCESSAR O LIFT {ex.Message}");
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




    }

}