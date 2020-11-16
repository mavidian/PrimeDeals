using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Data.Persistence;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace PrimeDeals.API.Controllers
{
   /// <summary>
   /// Special controller to manage commits of application data to persistent storage.
   /// Note that changes made to entities are transient, which, unless saved to persistent storage, will be discarded upon Web API shutdown.
   /// </summary>
   [ApiController]
   [Route("api/v{version:ApiVersion}/[controller]")]
   [SwaggerTag("Special resource to manage data persistence (use with caution!).")]
   [ApiVersion("1.0")]
   public class StorageController : ControllerBase
   {
     // This controller has direct access to unit of work and even persistor - this is because none of the repository updates ever commit the UOW.
     // Note that this Web API is intended for demo purposes, where every new session should start with the same data retrieved from persistent storage.
     // This behavior is different from typical repository/UOW implementations, e.g. using EF.

      private readonly IPersistor _persistor;
      private readonly IUnitOfWork _unitOfWork;

      public StorageController(IPersistor persistor, IUnitOfWork unitOfWork)
      {
         _persistor = persistor;
         _unitOfWork = unitOfWork;
      }


      /// <summary>
      /// Save current state of all entities to persistent storage (overwrites prior data).
      /// </summary>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - all entities have been persisted.</response>
      [HttpPut]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      public async Task<IActionResult> PersistStorage()
      {
         await _unitOfWork.CommitAsync();
         return NoContent();
      }


      /// <summary>
      /// Clear (reset) persistent storage.
      /// </summary>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - storage has been reset.</response>
      [HttpDelete]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      public IActionResult ClearStorage()
      {
         _persistor.Reset();
         return NoContent();
      }
   }
}
