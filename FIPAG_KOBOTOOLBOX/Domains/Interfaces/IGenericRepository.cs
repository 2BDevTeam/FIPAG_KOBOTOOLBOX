﻿//using FIPAG_KOBOTOOLBOX.Persistence.Contexts;

using Microsoft.EntityFrameworkCore;

namespace FIPAG_KOBOTOOLBOX.Domains.Interface
{
    public interface IGenericRepository<TContext> where TContext : DbContext
    {
        public void UpsertEntity<T>(T entity,List<string> keysToExclude, List<KeyValuePair<string, object>> conditions, bool saveChanges) where T : class;
        public void Add<T>(T entity) where T : class;

        public void BulkDelete<T>(IEnumerable<T> entityList) where T : class;
        public void BulkAdd<T>(IEnumerable<T> entityList) where T : class;

        public void BulkUpdate<T>(IEnumerable<T> entityList) where T : class;
        public void BulkOverWrite<T>(List<List<T>> entityLists) where T : class;
        public void BulkUpsertEntity<T>(List<T> entities, List<string> keyToExclude, bool saveChanges) where T : class;
        void SaveChanges();
    }
}
