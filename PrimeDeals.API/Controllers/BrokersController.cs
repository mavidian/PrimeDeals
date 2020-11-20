using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.API.Controllers
{
   [ApiController]
   [Route("api/v{version:ApiVersion}/[controller]")]
   [SwaggerTag("Top level entity; there can be many Sales for each Broker.")]
   [ApiVersion("1.0")]
   public class BrokersController : ControllerBase
   {
      private readonly IBrokerService _brokerService;

      public BrokersController(IBrokerService brokerService)
      {
         _brokerService = brokerService;
      }


      /// <summary>
      /// Obtain all Brokers.
      /// </summary>
      /// <returns>Asynchronous list of all brokers.</returns>
      /// <response code="200">OK - Brokers data returned.</response>
      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async IAsyncEnumerable<GetBrokerDTO> GetAll()
      {
         await foreach (var svcRslt in _brokerService.GetAllAsync()) yield return svcRslt.Value;
      }


      /// <summary>
      /// Obtain a given Broker.
      /// </summary>
      /// <param name="id">Id of the Broker.</param>
      /// <returns>The Broker.</returns>
      /// <response code="200">OK - Broker data returned.</response>
      /// <response code="404">Not Found - no Broker for a given id.</response>
      [HttpGet("{id}", Name = "GetBrokerById")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Get(string id)
      {
         var svcRslt = await _brokerService.GetByIdAsync(id);
         if (!svcRslt.Success) return NotFound();
         return Ok(svcRslt.Value);
      }


      /// <summary>
      /// Add a new Broker.
      /// </summary>
      /// <param name="broker">A Broker to create.</param>
      /// <param name="version">Current API version.</param>
      /// <returns>A newly created Broker.</returns>
      /// <response code="201">Created - Broker successfully created (and returned).</response>
      /// <response code="400">Bad Request - empty or bad data posted.</response>
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> Create([FromBody] SetBrokerDTO broker, ApiVersion version)
      {
         if (broker == null) return BadRequest();
         var svcRslt = await _brokerService.AddAsync(broker);
         if (! svcRslt.Success) return BadRequest(svcRslt.Message);
         return CreatedAtRoute("GetBrokerById", new { id = svcRslt.Value.Id, version = version.ToString() }, svcRslt.Value);
      }


      /// <summary>
      /// Replace an existing Broker.
      /// </summary>
      /// <param name="id">Id of the Broker to replace.</param>
      /// <param name="broker">The replacement Broker</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Broker successfully replaced.</response>
      /// <response code="400">Bad Request - empty or bad data posted, e.g. id mismatch.</response>
      /// <response code="404">Not Found - no Broker for a given id.</response>
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Replace(string id, [FromBody] SetBrokerDTO broker)
      {
         ////if (broker == null) return BadRequest();
         ////if (broker.Id != id) return BadRequest(this.BadRequestDetails("Id mismatch detected.", $"Id values are immutable; an attempt to change '{id}' into '{broker.Id}' is invalid."));
 
         var svcRslt = await _brokerService.ReplaceAsync(id, broker);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }


      /// <summary>
      /// Remove an existing Broker.
      /// </summary>
      /// <param name="id">ID of the Broker to remove.</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Broker successfully removed.</response>
      /// <response code="404">Not Found - no Broker for a given id.</response>
      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Delete(string id)
      {
         var svcRslt = await _brokerService.DeleteAsync(id);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }
   }
}
