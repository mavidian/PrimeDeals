using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.Core.Interfaces.Services
{
   /// <summary>
   /// Base interface for all services.
   /// </summary>
   /// <typeparam name="TGetDTO"></typeparam>
   /// <typeparam name="TAddDTO"></typeparam>
   /// <typeparam name="TReplaceDTO"></typeparam>
   public interface IService<TGetDTO,TAddDTO,TReplaceDTO>
   {
      IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync();
      IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync(string parentId);
      Task<IServiceResult<TGetDTO>> GetByIdAsync(string id);
      Task<IServiceResult<TGetDTO>> AddAsync(TAddDTO newEntity);  //return newly added entity w/Id
      Task<IServiceResult> ReplaceAsync(TReplaceDTO replacementEntity);
      Task<IServiceResult> DeleteAsync(string id);
   }
}