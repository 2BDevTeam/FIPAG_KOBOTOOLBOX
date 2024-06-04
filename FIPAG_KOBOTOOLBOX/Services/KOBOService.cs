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

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class KOBOService : IKOBOService
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboToolBoxAPI koboAPI = new KoboToolBoxAPI();


        private readonly IKOBORepository _KOBORespository;
        private readonly IGenericRepository _genericRepository;
        private readonly AppDbContext _appDbContext;

        public KOBOService( IKOBORepository KOBORepository, AppDbContext appDbContext)
        {
            _KOBORespository = KOBORepository;
            _appDbContext = appDbContext;
        }

        public KOBOService()
        {
        }

        public ResponseDTO GetResult(string nome)
        {
            var responseID = logHelper.generateResponseID();
            var response = koboAPI.GetResult(nome);

            return new ResponseDTO(new ResponseCodesDTO("0000", "Success", responseID), response, null);

        }
    }

}
