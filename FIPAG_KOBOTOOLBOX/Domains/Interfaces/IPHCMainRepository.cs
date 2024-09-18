using FIPAG_KOBOTOOLBOX.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPAG_KOBOTOOLBOX.Domains.Interfaces
{
    public interface IPHCMainRepository <TContext> where TContext : DbContext
    {

        public Cl GetClByNo(int no);
        public List<Cl> GetClients();
        public List<Consumos> GetConsumos();
        public Consumos GetConsumo(string ftstamp);
        public Cl2 GetCl2ByIdKobo(int idKobo);
        public Cl2 GetCl2ByStamp(string cl2stamp);
        public Ft GetFt(string ftstamp);
        public Ft2 GetFt2(string ft2stamp);
        public List<Ligacoes> GetClNaoSincronizadosLigacoes();
        public Ligacoes GetClNaoSincronizadosLigacoes(string clstamp);
        public decimal GetNoEm();
        public void DeleteLiftqueue(USyncQueue syncQueue);
        public UBasedados GetBaseDados(string nomeBd);
        public List<UBasedados> GetBaseDados();
        public List<ULibasedado> GetLiBaseDados(string Basedadosstamp);
        public ULibasedado GetFormID(string nome, string bdstamp);
    }
}
