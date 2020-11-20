using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeDeals.Data.Repositories
{
   /// <summary>
   /// Base implementation of all repositories.
   /// </summary>
   /// <typeparam name="TEntity"></typeparam>
   public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
   {
      //Marked abstract as derived class is needed to supply parameters to the NewId method (needed by AddAsync)
      //Implementation of IRepository<TEntity>, except for AddAsync method work fine w/o derived class.
      //This class can be "non-abstract", as long as NewId() method is virtual (not abstract) with some default implementation.

      private readonly Dictionary<string, TEntity> _entities;
      private readonly Random _randomForIdGen;

      private readonly HashSet<string> _idsInUse;

      public Repository(Dictionary<string, TEntity> entities)
      {
         _entities = entities;
         _idsInUse = new HashSet<string>(_entities.Keys); //in addition to keys in _entities, it will contain the ids "reserved" via GenerateId
         _randomForIdGen = new Random();
      }

      public async Task AddAsync(TEntity entity)
      {
         if (entity.Id == null) entity.Id = NewId();
         _entities.Add(entity.Id, entity);  //ArgumentException if entity already present (unlikely, but possible due to concurrency)
         await Task.CompletedTask;
      }

      public async Task<bool> DeleteAsync(string id)
      {
         return await Task.FromResult(_entities.Remove(id));
      }

      public async IAsyncEnumerable<TEntity> GetAllAsync()
      {
         foreach (var entity in _entities.Values)
         {
            yield return await Task.FromResult(entity);
         }
      }

      public virtual async IAsyncEnumerable<TEntity> GetAllAsync(string parentId)
      {
         foreach (var entity in _entities.Values.Where(v => v.ParentId == parentId))
         {
            yield return await Task.FromResult(entity);
         }
      }

      public async Task<TEntity> GetByIdAsync(string id)
      {
         TEntity entityToReturn;
         if (!_entities.TryGetValue(id, out entityToReturn)) entityToReturn = null;
         return await Task.FromResult(entityToReturn);
      }

      public async Task<bool> ReplaceAsync(string id, TEntity entity)
      {
         Debug.Assert(entity.Id == null);  //Ids are immutable, so no values allowed in the request body
         entity.Id = id;
         if (!_entities.ContainsKey(id)) return await Task.FromResult(false);
         _entities[id] = entity;
         return await Task.FromResult(true);
      }


      protected abstract string NewId();

      /// <summary>
      /// Generate random Id value that is not yet in use.
      /// To be called by derived class, e.g. BrokerRepository.
      /// </summary>
      /// <param name="idPrefix">E.g. B for broker</param>
      /// <param name="idLength">Number of digits in numerical part, e.g. 3 for broker.</param>
      /// <returns>Generated Id value, e.g. B101.</returns>
      protected string NewId(string idPrefix, int idLength)

      {
         string id = null;
         do
         {
            int numId = _randomForIdGen.Next(1, int.Parse(new string('9', idLength)));
            id = $"{idPrefix}{numId.ToString($"D{idLength}")}";
         } while (!_idsInUse.Add(id));

         return id;
      }

   }
}
