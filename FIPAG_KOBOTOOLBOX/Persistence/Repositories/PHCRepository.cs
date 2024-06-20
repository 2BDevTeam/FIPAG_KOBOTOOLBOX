using Microsoft.EntityFrameworkCore;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;
using static System.Net.WebRequestMethods;

namespace FIPAG_KOBOTOOLBOX.Persistence.Repositories
{
    public class PHCRepository<TContext> : IPHCRepository<TContext> where TContext : DbContext
    {

        private readonly TContext _context;

        public PHCRepository(TContext context)
        {
            _context = context;
        }


        public List<Cl> GetClients()
        {
            return _context.Set<Cl>()
                .ToList();
        }

        public Ft GetFt(string ftstamp)
        {
            return _context.Set<Ft>().
                FirstOrDefault(ft => ft.Ftstamp == ftstamp);
        }

        public Ft2 GetFt2(string ft2stamp)
        {
            return _context.Set<Ft2>().
                FirstOrDefault(ft => ft.Ft2stamp == ft2stamp);
        }

        public List<Ligacoes> GetClNaoSincronizadosLigacoes()
        {
            return _context.Set<Cl2>()
            .Join(_context.Set<Cl>(),
                  cl2 => cl2.Cl2stamp,
                  cl => cl.Clstamp,
                  (cl2, cl) => new { Cl2 = cl2, Cl = cl })
               .Where(joined => joined.Cl2.UKoboOri == true &&
                                joined.Cl.Tipo == "1-Activo" &&
                                joined.Cl2.UKoboSync == false
                                )
               .Select(joined => new Ligacoes
               {
                   Clstamp = joined.Cl.Clstamp,
                   No = (int)joined.Cl.No,
                   Nome = joined.Cl.Nome,
                   IDBenefKobo = (int)joined.Cl2.UKoboid,
                   dataLigacao = joined.Cl2.UInicio,
                   dataTermino= joined.Cl2.UTermino
               })
               .ToList();
        }

        public Ligacoes GetClNaoSincronizadosLigacoes(string clstamp)
        {
            return _context.Set<Cl2>()
            .Join(_context.Set<Cl>(),
                  cl2 => cl2.Cl2stamp,
                  cl => cl.Clstamp,
                  (cl2, cl) => new { Cl2 = cl2, Cl = cl })
               .Where(joined => joined.Cl.Clstamp == clstamp)
               .Select(joined => new Ligacoes
               {
                   Clstamp = joined.Cl.Clstamp,
                   No = (int)joined.Cl.No,
                   Nome = joined.Cl.Nome,
                   IDBenefKobo = (int)joined.Cl2.UKoboid,
                   dataLigacao = joined.Cl2.UInicio,
                   dataTermino = joined.Cl2.UTermino
               })
               .FirstOrDefault();
        }

        public Cl2 GetCl2PorIdKobo(int idKobo)
        {
            return _context.Set<Cl>()
                .Join(_context.Set<Cl2>(),
                      cl => cl.Clstamp,
                      cl2 => cl2.Cl2stamp,
                      (cl, cl2) => new { Cl = cl, Cl2 = cl2 })
                .Where(joined => joined.Cl2.UKoboid == idKobo)
                .Select(joined => joined.Cl2)
                .FirstOrDefault();
        }

        public Cl2 GetCl2PorStamp(string cl2stamp)
        {
            return _context.Set<Cl2>()
                .Where(joined => joined.Cl2stamp == cl2stamp)
                .FirstOrDefault();
        }

        public List<USyncQueue> GetUSyncQueue()
        {
            return _context.Set<USyncQueue>()
               .ToList();
        }


        public List<Consumos> GetConsumos()
        {
            DateTime today = DateTime.Today;
            DateTime specificDate = new DateTime(2024, 5, 27);

            return _context.Set<Ft>()
                .Join(_context.Set<Cl>(),
                      ft => ft.No,
                      cl => cl.No,
                      (ft, cl) => new { Ft = ft, Cl = cl })
                .Join(_context.Set<Ft3>(),
                      joined => joined.Ft.Ftstamp,
                      ft3 => ft3.Ft3stamp,
                      (joined, ft3) => new { joined.Ft, joined.Cl, Ft3 = ft3 })
                .Join(_context.Set<Ft2>(),
                      joined => joined.Ft.Ftstamp,
                      ft2 => ft2.Ft2stamp,
                      (joined, ft2) => new { joined.Ft, joined.Cl, joined.Ft3, Ft2 = ft2 })
                .Join(_context.Set<Cl2>(),
                      joined => joined.Cl.Clstamp,
                      cl2 => cl2.Cl2stamp,
                      (joined, cl2) => new { joined.Ft, joined.Cl, joined.Ft3, joined.Ft2, Cl2 = cl2 })
                .Where(joined => joined.Cl2.UKoboOri == true &&
                                 joined.Ft2.UKobosync == false &&
                                 joined.Ft.Ndoc == 1)
                .Select(joined => new Consumos
                {
                    Ftstamp = joined.Ft.Ftstamp,
                    Clstamp = joined.Cl.Clstamp,
                    No = (int)joined.Ft.No,
                    Nome = joined.Ft.Nome,
                    IDBenefKobo = (int)joined.Cl2.UKoboid,
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


        public Consumos GetConsumo(string ftstamp)
        {
            DateTime today = DateTime.Today;
            DateTime specificDate = new DateTime(2024, 5, 27);

            return _context.Set<Ft>()
                .Join(_context.Set<Cl>(),
                      ft => ft.No,
                      cl => cl.No,
                      (ft, cl) => new { Ft = ft, Cl = cl })
                .Join(_context.Set<Ft3>(),
                      joined => joined.Ft.Ftstamp,
                      ft3 => ft3.Ft3stamp,
                      (joined, ft3) => new { joined.Ft, joined.Cl, Ft3 = ft3 })
                .Join(_context.Set<Cl2>(),
                      joined => joined.Cl.Clstamp,
                      cl2 => cl2.Cl2stamp,
                      (joined, cl2) => new { joined.Ft, joined.Cl, joined.Ft3, Cl2 = cl2 })
                .Where(joined => joined.Ft.Ftstamp == ftstamp)
                .Select(joined => new Consumos
                {
                    Ftstamp = joined.Ft.Ftstamp,
                    Clstamp = joined.Cl.Clstamp,
                    No = (int)joined.Ft.No,
                    Nome = joined.Ft.Nome,
                    IDBenefKobo = (int)joined.Cl2.UKoboid,
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
        }


        public decimal GetNoEm()
        {
            return _context.Set<Em>()
                         .Select(em => em.No)
                         .ToList()
                         .DefaultIfEmpty(0)
                         .Max() + 1;
        }

        public void DeleteLiftqueue(USyncQueue syncQueue)
        {
            _context.Set<USyncQueue>().Remove(syncQueue);
        }

        public UBasedados GetBaseDados(string nomeBd)
        {
            return _context.Set<UBasedados>()
               .FirstOrDefault(bd => bd.Nomebd == nomeBd);
        }

        public List<ULibasedado> GetLiBaseDados(string Basedadosstamp)
        {
            return _context.Set<ULibasedado>()
                .Where(bd => bd.Basedadosstamp == Basedadosstamp)
               .ToList();
        }

        public ULibasedado GetFormID(string nome, string bdstamp)
        {
            return _context.Set<ULibasedado>()
            .Where(f => f.SubNome == nome
                    && f.Basedadosstamp == bdstamp)
            .ToList().FirstOrDefault();
        }

    }
}

//KOBORepository