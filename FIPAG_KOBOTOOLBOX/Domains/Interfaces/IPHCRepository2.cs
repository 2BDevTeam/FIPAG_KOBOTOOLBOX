using FIPAG_KOBOTOOLBOX.Domains.Models;
using Microsoft.EntityFrameworkCore;

namespace FIPAG_KOBOTOOLBOX.Domains.Interfaces
{
    public interface IPHCRepository2 <TContext> where TContext : DbContext
    {

        public List<Bo> GetBoTeste(TContext _context);
        public List<Cl> GetClients(TContext _context);
        public List<Consumos> GetConsumos(TContext _context);
        public Consumos GetConsumo(TContext _context, string ftstamp);
        public List<USyncQueue> GetUSyncQueue(TContext _context, string nomeTab);
        public Cl2 GetCl2PorIdKobo(TContext _context,long idKobo);
        public Cl2 GetCl2PorStamp(TContext _context, string cl2stamp);
        public Ft GetFt(TContext _context, string ftstamp);
        public Ft2 GetFt2(TContext _context, string ft2stamp);
        public List<Ligacoes> GetClNaoSincronizadosLigacoes(TContext _context);
        public Ligacoes GetClNaoSincronizadosLigacoes(TContext _context, string clstamp);
        public decimal GetNoEm(TContext _context);
        public void DeleteSyncQueue(TContext _context, USyncQueue syncQueue);



        public void UpsertEntity<T>(TContext appDbContext, T entity, List<string> keysToExclude, List<KeyValuePair<string, object>> conditions, bool saveChanges) where T : class;
        public void Add<T>(TContext appDbContext, T entity) where T : class;

        public void BulkDelete<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class;
        public void BulkAdd<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class;

        public void BulkUpdate<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class;
        public void BulkOverWrite<T>(TContext appDbContext, List<List<T>> entityLists) where T : class;
        public void BulkUpsertEntity<T>(TContext appDbContext, List<T> entities, List<string> keyToExclude, bool saveChanges) where T : class;
        void SaveChanges(TContext appDbContext);
    }

}
