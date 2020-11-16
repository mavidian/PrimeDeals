using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;
using PrimeDeals.Data.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeDeals.Data.Repositories
{
   /// <summary>
   /// This unit of work (UOW) is intended to encompass "large" set of updates (like a "demo" session) and not individual updates (like add policy).
   /// Note that there is no mechansims to track changes, so commit needs to replaces entire storage (as opposed to just apply differences).
   /// Also note that state of all repos is retrieved form storage upon UOW instantiation and then persisted upon calling CommitAsync.
   /// </summary>
   public class UnitOfWork : IUnitOfWork
   {
      public IBrokerRepository BrokerRepo { get; set; }
      public ISaleRepository SaleRepo { get; set; }
      public IPolicyRepository PolicyRepo { get; set; }

      private IPersistor _persistor;

      public UnitOfWork(IPersistor persistor)
      {
         _persistor = persistor;

         var storage = _persistor.RestoreObject<SummaryStorage>("SUMMARY");
         var brokers = storage != null ? RestoreEntities<Broker>(storage.BrokerIds, IBrokerRepository.StoragePrefix)
                                       : new Dictionary<string, Broker>();
         BrokerRepo = new BrokerRepository(brokers);
         var sales = storage != null ? RestoreEntities<Sale>(storage.SaleIds, ISaleRepository.StoragePrefix)
                                     : new Dictionary<string, Sale>();
         SaleRepo = new SaleRepository(sales);
         var policies = storage != null ? RestoreEntities<Policy>(storage.PolicyIds, IPolicyRepository.StoragePrefix)
                                     : new Dictionary<string, Policy>();
         PolicyRepo = new PolicyRepository(policies);
      }


      /// <summary>
      /// Helper method to restore a dictionary of entities (of a given type) from persistent storage.
      /// </summary>
      /// <typeparam name="TEntity">E.g. Broker.</typeparam>
      /// <param name="entityIds">IDs of entities to restore.</param>
      /// <param name="storagePrefix">E.g. BrokerState_ for a broker entity.</param>
      /// <returns></returns>
      private Dictionary<string, TEntity> RestoreEntities<TEntity>(IEnumerable<string> entityIds, string storagePrefix) where TEntity : class, IEntity
      {
         //consider making this method async (even though it's called by ctor).
         return entityIds.Select(id => _persistor.RestoreObject<TEntity>(storagePrefix + id))
                         .Where(s => s != null)  //remove objects that could not be restored
                         .ToDictionary(s => s.Id, s => s);
      }


      /// <summary>
      /// Elements to persist in AppState
      /// </summary>
      private class SummaryStorage
      {
         public List<string> BrokerIds;
         public List<string> SaleIds;
         public List<string> PolicyIds;
      }


      /// <summary>
      /// Asynchronously save repositories to permanent storage.
      /// </summary>
      /// <returns>Task with total number of elements persisted.</returns>
      public async Task<int> CommitAsync()
      {
         //First, remove all existing elements from storage
         _persistor.Reset();

         var brokerIds = await BrokerRepo.GetAllAsync().Select(b => b.Id).ToListAsync();
         var saleIds = await SaleRepo.GetAllAsync().Select(b => b.Id).ToListAsync();
         var policyIds = await PolicyRepo.GetAllAsync().Select(b => b.Id).ToListAsync();

         //Ids of entities to be stored
         var storage = new SummaryStorage()
         {
            BrokerIds = brokerIds,
            SaleIds = saleIds,
            PolicyIds = policyIds
         };

         _persistor.PersistObject("SUMMARY", storage);

         //Save all brokers
         await BrokerRepo.GetAllAsync().ForEachAsync(b => _persistor.PersistObject(IBrokerRepository.StoragePrefix + b.Id, b ));

         //Save all sales
         await SaleRepo.GetAllAsync().ForEachAsync(s => _persistor.PersistObject(ISaleRepository.StoragePrefix + s.Id, s));

         //Save all policies
         await PolicyRepo.GetAllAsync().ForEachAsync(p => _persistor.PersistObject(IPolicyRepository.StoragePrefix + p.Id, p));

         return 1 + brokerIds.Count + saleIds.Count; //total number of stored elements
      }


      public void Dispose()
      {
         //nothing to do here? (persistor could be disposed here)
      }
   }
}
