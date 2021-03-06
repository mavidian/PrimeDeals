﻿using PrimeDeals.Core.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.Core.Interfaces.Repositories
{
   /// <summary>
   /// Base interface for all repositories.
   /// </summary>
   /// <typeparam name="TEntity"></typeparam>
   public interface IRepository<TEntity> where TEntity : IEntity
   {
      IAsyncEnumerable<TEntity> GetAllAsync();
      IAsyncEnumerable<TEntity> GetAllAsync(string parentId);
      Task<TEntity> GetByIdAsync(string id);
      Task AddAsync(TEntity entity);
      Task<bool> ReplaceAsync(string id, TEntity entity);
      Task<bool> DeleteAsync(string id);

      bool ContainsId(string id);
      bool ContainsParentId(string parentId); //true if one or more records point(s) to a given parent

      //"default" values to be "overridden" in derived interfaces:
      static string StoragePrefix => "GenericState_";
      static string IdPrefix => "X";
      static int IdLength => 1;
      //TODO: consider relocating the above "default" values, e.g. as "Configurations" injected into UnitOfWork
   }
}
