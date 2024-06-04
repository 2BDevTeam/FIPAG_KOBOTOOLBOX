using Microsoft.EntityFrameworkCore;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;
using static System.Net.WebRequestMethods;

namespace FIPAG_KOBOTOOLBOX.Persistence.Repositories
{
    public class KOBORepository : IKOBORepository
    {
        private readonly AppDbContext _appDbContext;

        public KOBORepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<Cl> GetClients()
        {
            return _appDbContext.Cl.ToList();
        }

    }
}

//OPPRepository