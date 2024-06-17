﻿using FIPAG_KOBOTOOLBOX.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPAG_KOBOTOOLBOX.Domains.Interfaces
{
    public interface IPHCRepository <TContext> where TContext : DbContext
    {

        public List<Cl> GetClients();

        public List<Consumos> GetConsumos();
        public Consumos GetConsumo(string ftstamp);
        public List<USyncQueue> GetUSyncQueue();
        public Cl GetClPorIdKobo(int idKobo);
        public Ft GetFt(string ftstamp);
        public Ft2 GetFt2(string ft2stamp);
        public List<Ligacoes> GetClNaoSincronizadosLigacoes();
        public Ligacoes GetClNaoSincronizadosLigacoes(string clstamp);
        public decimal GetNoEm();
    }
}