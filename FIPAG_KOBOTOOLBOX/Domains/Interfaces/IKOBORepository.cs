using FIPAG_KOBOTOOLBOX.Domains.Models;

namespace FIPAG_KOBOTOOLBOX.Domains.Interfaces
{
    public interface IKOBORepository
    {

        public List<Cl> GetClients();
        public List<Consumos> GetConsumos();
        public Cl GetClPorIdKobo(int idKobo);
        public Ft GetFt(string ftstamp);
        public List<Cl> GetClNaoSincronizados();
        public decimal GetNoEm();
    }
}
