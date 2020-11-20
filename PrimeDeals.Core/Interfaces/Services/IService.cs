using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.Core.Interfaces.Services
{
   /// <summary>
   /// Base interface for all services.
   /// </summary>
   /// <typeparam name="TGetDTO"></typeparam>
   /// <typeparam name="TSetDTO"></typeparam>
   public interface IService<TGetDTO,TSetDTO>
   {
      IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync();
      IAsyncEnumerable<IServiceResult<TGetDTO>> GetAllAsync(string parentId);
      Task<IServiceResult<TGetDTO>> GetByIdAsync(string id);
      Task<IServiceResult<TGetDTO>> AddAsync(TSetDTO newEntity);  //return newly added entity w/Id
      Task<IServiceResult> ReplaceAsync(string id, TSetDTO replacementEntity);
      Task<IServiceResult> DeleteAsync(string id);
   }
}