using Microsoft.EntityFrameworkCore;
using FIPAG_KOBOTOOLBOX.Domains.Interfaces;
using FIPAG_KOBOTOOLBOX.Domains.Models;
using FIPAG_KOBOTOOLBOX.Persistence.Contexts;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;
using static System.Net.WebRequestMethods;
using FIPAG_KOBOTOOLBOX.DTOs;
using FIPAG_KOBOTOOLBOX.Extensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIPAG_KOBOTOOLBOX.Persistence.Repositories
{
    public class PHCRepository2<TContext> : IPHCRepository2<TContext> where TContext : DbContext
    {

        private readonly ConversionExtension conversionExtension=new ConversionExtension();

        public List<Cl> GetClients(TContext _context)
        {
            return _context.Set<Cl>()
                .ToList();
        }
        public List<Bo> GetBoTeste(TContext _context)
        {
            return _context.Set<Bo>()
                .Where(bo => bo.Boano==2024)
                .ToList();
        }

        public Ft GetFt(TContext _context, string ftstamp)
        {
            return _context.Set<Ft>().
                FirstOrDefault(ft => ft.Ftstamp == ftstamp);
        }

        public Ft2 GetFt2(TContext _context, string ft2stamp)
        {
            return _context.Set<Ft2>().
                FirstOrDefault(ft => ft.Ft2stamp == ft2stamp);
        }

        public List<Ligacoes> GetClNaoSincronizadosLigacoes(TContext _context)
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

        public Ligacoes GetClNaoSincronizadosLigacoes(TContext _context, string clstamp)
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

        public Cl2 GetCl2PorIdKobo(TContext _context, int idKobo)
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

        public Cl2 GetCl2PorStamp(TContext _context, string cl2stamp)
        {
            return _context.Set<Cl2>()
                .Where(joined => joined.Cl2stamp == cl2stamp)
                .FirstOrDefault();
        }

        public List<USyncQueue> GetUSyncQueue(TContext _context, string nomeTab)
        {
            DateTime today = DateTime.Today;
            DateTime yesterday= today.AddDays(-1);

            return _context.Set<USyncQueue>()
                .Where(sq=> sq.Nometabela == nomeTab
                        //&& sq.Ousrdata.Date != today
                        && sq.Ousrdata.Date != yesterday
                        )
               .ToList();
        }

        public List<Consumos> GetConsumos(TContext _context)
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


        public Consumos GetConsumo(TContext _context, string ftstamp)
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


        public decimal GetNoEm(TContext _context)
        {
            return _context.Set<Em>()
                         .Select(em => em.No)
                         .ToList()
                         .DefaultIfEmpty(0)
                         .Max() + 1;
        }

        
        public void DeleteSyncQueue(TContext _context, USyncQueue syncQueue)
        {
            _context.Set<USyncQueue>().Remove(syncQueue);
        }









        /**  Classes Genericas  **/
        public void SaveChanges(TContext appDbContext)
        {
            appDbContext.SaveChanges();
        }

        public void BulkOverWrite<T>(TContext appDbContext, List<List<T>> entityLists) where T : class
        {
            var transaction = appDbContext.Database.BeginTransaction();
            try
            {
                foreach (var list in entityLists)
                {
                    appDbContext.RemoveRange(list);
                }

                appDbContext.SaveChanges(); // Remove data*/

                foreach (var list in entityLists)
                {
                    appDbContext.AddRange(list);
                }

                appDbContext.SaveChanges(); // Add new data

                transaction.Commit();
            }
            catch (Exception ex)
            {

                Debug.Print("ROLLING BACK BulkOverWrite ");
                transaction.Rollback();
                throw new Exception("BULKOVERRITE EXEPTION: " + ex.Message + "INNER GenericRepository Repository: " + ex.InnerException + " Stack BulkOverWrite Repository: " + ex.StackTrace);

            }
        }

        public void BulkUpsertEntity<T>(TContext appDbContext, List<T> entities, List<string> keysToExclude, bool saveChanges) where T : class
        {

            foreach (var entity in entities)
            {
                var conditions = new List<KeyValuePair<string, object>>();

                var keyToExclude = keysToExclude.FirstOrDefault();

                if (keyToExclude != null)
                {
                    conditions.Add(new KeyValuePair<string, object>(keyToExclude, entity.GetValObjDy(keyToExclude)));

                }

                UpsertEntity(appDbContext,entity, keysToExclude, conditions, saveChanges);
            }

        }

        public void Add<T>(TContext appDbContext, T entity) where T : class
        {
            entity.AssignDefaultEntityValues();
            appDbContext.Add(entity);
        }

        static bool IsPropertyInList(PropertyInfo searchProperty, List<PropertyInfo> propertyList)
        {
            return propertyList.Where(propert => propert.Name == searchProperty.Name).Any();
        }

        public void UpsertEntity<T>(TContext appDbContext, T entity, List<string> keysToExclude, List<KeyValuePair<string, object>> conditions, bool saveChanges) where T : class
        {
            var entityType = typeof(T);

            List<PropertyInfo> keysToExcludeProperties = new List<PropertyInfo>();

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new IgnoreNullAndDefaultValuesContractResolver(),
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(entity, settings);
            var entityObject = JObject.Parse(json);

            var modifiedProperties = entity.GetAssignedProperties();

            foreach (var keyToExclude in keysToExclude)
            {
                var keyToExcludeProperty = entityType.GetProperty(keyToExclude);

                if (keyToExcludeProperty == null)
                {
                    throw new InvalidOperationException($"Entity does not have a property named '{keyToExclude}'.");
                }

                keysToExcludeProperties.Add(keyToExcludeProperty);
            }

            // Add more conditions as needed
            var parameter = Expression.Parameter(entityType, "e");
            Expression combinedCondition = null;

            foreach (var condition in conditions)
            {
                var keyProperty = entityType.GetProperty(condition.Key);
                if (keyProperty == null)
                {
                    throw new InvalidOperationException($"Entity does not have a property named '{keyProperty}'.");
                }
                var property = entityType.GetProperty(condition.Key);
                var propertyValue = condition.Value;
                var propertyExpression = Expression.Property(parameter, property);
                var constant = Expression.Constant(propertyValue);
                var equalExpression = Expression.Equal(propertyExpression, constant);

                if (combinedCondition == null)
                {
                    combinedCondition = equalExpression;
                }
                else
                {
                    combinedCondition = Expression.AndAlso(combinedCondition, equalExpression);
                }
            }

            var lambda = Expression.Lambda<Func<T, bool>>(combinedCondition, parameter);

            Debug.Print($"Combined {combinedCondition}");

            var existingEntity = appDbContext.Set<T>().Where(lambda).FirstOrDefault();

            if (existingEntity != null)
            {
                Debug.Print($"EXISTING ENTITY NOT NULL  ");
                foreach (var property in entityType.GetProperties())
                {
                    if (IsPropertyInList(property, keysToExcludeProperties) == false)
                    {
                        var propertyName = property.Name.ToString();

                        if (modifiedProperties.Contains(propertyName))
                        {
                            var newValue = property.GetValue(entity);

                            if (newValue != null)
                            {
                                var finalValue = newValue;
                                if (property.PropertyType == typeof(DateTime))
                                {

                                    finalValue = conversionExtension.ParseToDate(newValue);
                                }
                                property.SetValue(existingEntity, finalValue);
                            }
                        }
                    }
                }

                appDbContext.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                appDbContext.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                Debug.Print("New Entityy");

                // var entitytoAdd = ;
                entity.AssignDefaultValues();
                appDbContext.Set<T>().Add(entity);
            }

            if (saveChanges)
            {
                appDbContext.SaveChanges();

            }
        }

        public bool IsPropertySet<T>(TContext appDbContext, T obj, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property == null) return false;

            var value = property.GetValue(obj);
            var defaultValue = Activator.CreateInstance(property.PropertyType);
            return !Equals(value, defaultValue);
        }
        
        public void BulkAdd<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class
        {
            appDbContext.AddRange(entityList);
        }

        public void BulkUpdate<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class
        {
            appDbContext.UpdateRange(entityList);
        }

        public void BulkDelete<T>(TContext appDbContext, IEnumerable<T> entityList) where T : class
        {
            appDbContext.RemoveRange(entityList);
        }

    }
}

//KOBORepository