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

namespace FIPAG_KOBOTOOLBOX.Services
{
    public class PHCService 
    {
        private readonly LogHelper logHelper = new LogHelper();
        private readonly KoboAPI koboAPI = new KoboAPI();


        //private readonly IPHCRepository _PHCRespository;
        //private readonly IGenericRepository _genericRepository;
        private readonly AppDbContextOnBD _appDbContext;

        private readonly IPHCRepository<AppDbContextOnBD> _phcRepository1;
        public PHCService(IPHCRepository<AppDbContextOnBD> phcRepository1)
        {
            _phcRepository1 = phcRepository1;
        }

        public Ft GetFtFromContext1(string ftstamp)
        {
            return _phcRepository1.GetFt(ftstamp);
        }

        public Ft2 GetFt2FromContext1(string ft2stamp)
        {
            return _phcRepository1.GetFt2(ft2stamp);
        }

        /*
        public Ft GetFtFromContext2(string ftstamp)
        {
            return _phcRepository2.GetFt(ftstamp);
        }

        public Ft2 GetFt2FromContext2(string ft2stamp)
        {
            return _phcRepository2.GetFt2(ft2stamp);
        }
        */

    }

}
