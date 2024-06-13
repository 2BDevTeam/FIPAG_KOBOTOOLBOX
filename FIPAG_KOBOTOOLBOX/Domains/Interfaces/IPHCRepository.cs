using FIPAG_KOBOTOOLBOX.Domains.Models;

namespace FIPAG_KOBOTOOLBOX.Domains.Interfaces
{
    public interface IPHCRepository
    {

        public List<Cl> GetClients();
        public List<Consumos> GetConsumos();
        public Cl GetClPorIdKobo(int idKobo);
        public Ft GetFt(string ftstamp);
        public List<Ligacoes> GetClNaoSincronizadosLigacoes();
        public decimal GetNoEm();
    }
}
