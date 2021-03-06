﻿using AutoMapper;
using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Services
{
   /// <summary>
   /// Base implementation of all services.
   /// </summary>
   /// <typeparam name="TEntity"></typeparam>
   /// <typeparam name="TGetDTO"></typeparam>
   /// <typeparam name="TSetDTO"></typeparam>
   /// <typeparam name="TReplaceDTO"></typeparam>
   public abstract class Service<TEntity, TGetDTO, TSetDTO> : IService<TGetDTO, TSetDTO> where TEntity : IEntity
   {
      //Marked abstract as derived ctor is needed to supply the required repolistory.

      private readonly IMapper _mapper;
      protected IRepository<TEntity> _repository;

      public Service(IMapper mapper)
      {
         _mapper = mapper;
      }

      public async Task<IServiceResult<TGetDTO>> AddAsync(TSetDTO newEntity)
      {
         var entity = _mapper.Map<TEntity>(newEntity);
         await _repository.AddAsync(entity);  //will assign Id
         return new ServiceResult<TGetDTO> { Value = _mapper.Map<TGetDTO>(entity) };
      }

      public virtual bool ContainsParentId(string parentId)
      {
         return _repository.ContainsParentId(parentId);
      }

      public async Task<IServiceResult> DeleteAsync(string id)
      {
         var success = await _repository.DeleteAsync(id);
         return new ServiceResult { Success = success };
      }

      public async IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync()
      {
         await foreach (var entity in _repository.GetAllAsync())
         {
            yield return new ServiceResult<TGetDTO> { Value = _mapper.Map<TGetDTO>(entity) };
         }
      }

      public virtual async IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync(string parentId)
      {
         await foreach (var entity in _repository.GetAllAsync(parentId))
         {
            yield return new ServiceResult<TGetDTO> { Value = _mapper.Map<TGetDTO>(entity) };
         }
      }

      public async Task<IServiceResult<TGetDTO>> GetByIdAsync(string id)
      {
         var entity = await _repository.GetByIdAsync(id);
         return new ServiceResult<TGetDTO> { Success = entity != null, Value = _mapper.Map<TGetDTO>(entity) };
      }

      public async Task<IServiceResult> ReplaceAsync(string id, TSetDTO replacementEntity)
      {
         var success = await _repository.ReplaceAsync(id, _mapper.Map<TEntity>(replacementEntity));
         return new ServiceResult { Success = success };
      }
   }
}
