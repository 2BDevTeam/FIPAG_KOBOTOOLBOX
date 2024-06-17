using Microsoft.EntityFrameworkCore;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;
using static System.Net.WebRequestMethods;

namespace FIPAG_KOBOTOOLBOX.Persistence.Repositories
{
    public class PHCRepository : IPHCRepository
    {
        private readonly AppDbContext _appDbContext;

        public PHCRepository(AppDbContext appDbContext)
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

        public Ft2 GetFt2(string ft2stamp)
        {
            return _appDbContext.Ft2.
                FirstOrDefault(ft => ft.Ft2stamp == ft2stamp);
        }

        public List<Ligacoes> GetClNaoSincronizadosLigacoes()
        {
            return _appDbContext.Cl2
            .Join(_appDbContext.Cl,
                  cl2 => cl2.Cl2stamp,
                  cl => cl.Clstamp,
                  (cl2, cl) => new { Cl2 = cl2, Cl = cl })
               .Where(joined => joined.Cl.UKoboOri == true &&
                                joined.Cl.Tipo == "1-Activo" &&
                                joined.Cl.UKoboSync == false
                                )
               .Select(joined => new Ligacoes
               {
                   Clstamp = joined.Cl.Clstamp,
                   No = (int)joined.Cl.No,
                   Nome = joined.Cl.Nome,
                   IDBenefKobo = (int)joined.Cl.UKoboid,
                   dataLigacao = joined.Cl2.UIniciof
               })
               .ToList();
        }

        public Ligacoes GetClNaoSincronizadosLigacoes(string clstamp)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _appDbContext.Cl2
            .Join(_appDbContext.Cl,
                  cl2 => cl2.Cl2stamp,
                  cl => cl.Clstamp,
                  (cl2, cl) => new { Cl2 = cl2, Cl = cl })
               .Where(joined => joined.Cl.UKoboOri == true &&
                                joined.Cl.Tipo == "1-Activo" &&
                                joined.Cl.UKoboSync == false
                                )
               .Select(joined => new Ligacoes
               {
                   Clstamp = joined.Cl.Clstamp,
                   No = (int)joined.Cl.No,
                   Nome = joined.Cl.Nome,
                   IDBenefKobo = (int)joined.Cl.UKoboid,
                   dataLigacao = joined.Cl2.UIniciof
               })
               .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public Cl GetClPorIdKobo(int idKobo)
        {
            return _appDbContext.Cl
                .FirstOrDefault(cl => cl.UKoboid == idKobo);
        }


        public List<USyncQueue> GetUSyncQueue()
        {
            return _appDbContext.USyncqueue
               .ToList();
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
                .Join(_appDbContext.Ft2,
                      joined => joined.Ft.Ftstamp,
                      ft2 => ft2.Ft2stamp,
                      (joined, ft2) => new { joined.Ft, joined.Cl, joined.Ft3, Ft2 = ft2 })
                .Where(joined => joined.Cl.UKoboOri == true &&
                                 joined.Ft2.UKobosync == false &&
                                 joined.Ft.Ndoc == 1)
                .Select(joined => new Consumos
                {
                    Ftstamp = joined.Ft.Ftstamp,
                    Clstamp = joined.Cl.Clstamp,
                    No = (int)joined.Ft.No,
                    Nome = joined.Ft.Nome,
                    IDBenefKobo = (int)joined.Cl.UKoboid,
                    Nmdoc = joined.Ft.Nmdoc,
                    Fno = (int)joined.Ft.Fno,
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


        public Consumos GetConsumo(string ftstamp )
        {
            DateTime today = DateTime.Today;
            DateTime specificDate = new DateTime(2024, 5, 27);

#pragma warning disable CS8603 // Possible null reference return.
            return _appDbContext.Ft
                .Join(_appDbContext.Cl,
                      ft => ft.No,
                      cl => cl.No,
                      (ft, cl) => new { Ft = ft, Cl = cl })
                .Join(_appDbContext.Ft3,
                      joined => joined.Ft.Ftstamp,
                      ft3 => ft3.Ft3stamp,
                      (joined, ft3) => new { joined.Ft, joined.Cl, Ft3 = ft3 })
                .Where(joined => joined.Ft.Ftstamp == ftstamp)
                .Select(joined => new Consumos
                {
                    Ftstamp = joined.Ft.Ftstamp,
                    Clstamp = joined.Cl.Clstamp,
                    No = (int)joined.Ft.No,
                    Nome = joined.Ft.Nome,
                    IDBenefKobo = (int)joined.Cl.UKoboid,
                    Nmdoc = joined.Ft.Nmdoc,
                    Fno = (int)joined.Ft.Fno,
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
                .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
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