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
        public Cl2 GetCl2PorIdKobo(int idKobo);
        public Cl2 GetCl2PorStamp(string cl2stamp);
        public Ft GetFt(string ftstamp);
        public Ft2 GetFt2(string ft2stamp);
        public List<Ligacoes> GetClNaoSincronizadosLigacoes();
        public Ligacoes GetClNaoSincronizadosLigacoes(string clstamp);
        public decimal GetNoEm();
        public void DeleteLiftqueue(USyncQueue syncQueue);
        public UBasedados GetBaseDados(string nomeBd);
        public List<ULibasedado> GetLiBaseDados(string Basedadosstamp);
        public ULibasedado GetFormID(string nome, string bdstamp);
    }
}
