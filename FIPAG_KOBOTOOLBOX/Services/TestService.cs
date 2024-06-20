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
    public class TestService : ITestService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboAPI koboAPI = new KoboAPI();


        //private readonly IPHCRepository _PHCRespository;
        private readonly IPHCMainRepository<AppDbContextMain> _phcRepositoryOnBD;

        public TestService(IPHCMainRepository<AppDbContextMain> OnBD_Repository)
        {
            _phcRepositoryOnBD = OnBD_Repository;
        }


        public TestService()
        {

        }



        public async Task ProcessarFormularios(string nomeBd)
        {

            try
            {
                var bd = _phcRepositoryOnBD.GetBaseDados(nomeBd);
                var formularios = _phcRepositoryOnBD.GetLiBaseDados(bd.UBasedadosstamp);

                var connectionString = $"Server={bd.Nomesrv.Trim()};Database={bd.Nomebd.Trim()};User Id={bd.Username.Trim()};Password={bd.Password.Trim()};Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False";
                var connectionString2 = "Server=SRV05\\SQLDEV2019;Database=OnBD_FIPAG;User Id=isac.munguambe;Password=Murd3rB4nd;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False";
                Debug.Print($"Connection   {connectionString}");


                var dynamicContext = new DynamicContext(connectionString);


                var aux = dynamicContext.Bo
                    .Where(bo => bo.Boano == 2024)
                    .ToList();
                Debug.Print($"asdasd   {aux.Count}");
                
                

            }
            catch (Exception ex)
            {
                var errorDTO = new ErrorDTO { message = ex?.Message, stack = ex?.StackTrace?.ToString(), inner = ex?.InnerException?.ToString() + "  " };
                Debug.Print($"ProcessarFormularios ERROR DTO {errorDTO}");
                var finalResponse = new ResponseDTO(new ResponseCodesDTO("0007", "Error", logHelper.generateResponseID()), errorDTO.ToString(), null);
                logHelper.generateResponseLogJB(finalResponse, logHelper.generateResponseID().ToString(), "ProcessarFormularios", errorDTO?.ToString());
            }

        }


    }

}
