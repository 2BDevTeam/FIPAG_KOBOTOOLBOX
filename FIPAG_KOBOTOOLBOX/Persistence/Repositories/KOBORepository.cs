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

        public Ft GetFt(string ftstamp)
        {
            return _appDbContext.Ft.
                FirstOrDefault(ft => ft.Ftstamp == ftstamp);
        }

        public List<Cl> GetClNaoSincronizados()
        {
            return _appDbContext.Cl
                .Where(cl => cl.UKoboOri == true && cl.UKoboSync == false)
                .ToList();
        }

        public Cl GetClPorIdKobo(int idKobo)
        {
            return _appDbContext.Cl
                .FirstOrDefault(cl => cl.UKoboid == idKobo);
        }

        public List<Consumos> GetConsumos()
        {
            DateTime today = DateTime.Today;
            DateTime specificDate = new DateTime(2024, 5, 27);

            return _appDbContext.Ft
                .Join(_appDbContext.Cl,
                      ft => ft.No,
                      cl => cl.No,
                      (ft, cl) => new { Ft = ft, Cl = cl })
                .Join(_appDbContext.Ft3,
                      joined => joined.Ft.Ftstamp,
                      ft3 => ft3.Ft3stamp,
                      (joined, ft3) => new { joined.Ft, joined.Cl, Ft3 = ft3 })
                .Where(joined => joined.Cl.UKoboOri == true &&
                                 joined.Ft.UKobosync == false &&
                                 joined.Ft.Ndoc == 1
                                 //joined.Ft.Fdata == specificDate
                                 )
                .Select(joined => new Consumos
                {
                    Ftstamp = joined.Ft.Ftstamp,
                    Clstamp = joined.Cl.Clstamp,
                    No = (int)joined.Ft.No,
                    Nome = joined.Ft.Nome,
                    IDBenefKobo = (int) joined.Cl.UKoboid,
                    Nmdoc = joined.Ft.Nmdoc,
                    Fno = (int)joined.Ft.Fno,
                    ConsumoMensal = 0,
                    TotalMeticais = joined.Ft.Total,
                    Fdata = joined.Ft.Fdata,
                    TipoFatura = joined.Ft3.UTipofac,
                    Periodo = joined.Ft3.UPeriodo,
                    LeituraAnterior = joined.Ft3.ULeiant,
                    LeituraActual = joined.Ft3.ULeiact,
                    ConsumoFaturado = joined.Ft3.UFactu,
                    ConsumoReal = joined.Ft3.UReal,
                    Anomalia = joined.Ft3.UAnomal
                })
                .ToList();
        }

        public decimal GetNoEm()
        {
            return _appDbContext.Em
                         .Select(em => em.No)
                         .ToList()
                         .DefaultIfEmpty(0)
                         .Max() + 1;
        }
    }
}

//KOBORepository