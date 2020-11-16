using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeDeals.Core.DTOs.Sale;
using PrimeDeals.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.API.Controllers
{
   [ApiController]
   [Route("api/v{version:ApiVersion}/[controller]")]
   [SwaggerTag("Each Sale belongs to a Broker; there can be many Policies for each Sale.")]
   [ApiVersion("1.0")]
   public class SalesController : ControllerBase
   {
      private readonly ISaleService _saleService;

      public SalesController(ISaleService saleService)
      {
         _saleService = saleService;
      }


      /// <summary>
      /// Obtain all Sales.
      /// </summary>
      /// <returns>Asynchronous list of all Sales.</returns>
      /// <response code="200">OK - Sales data returned.</response>
      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async IAsyncEnumerable<GetSaleDTO> GetAll()
      {
         await foreach (var svcRslt in _saleService.GetAllAsync()) yield return svcRslt.Value;
      }


      /// <summary>
      /// Obtain all Sales for a given Broker.
      /// </summary>
      /// <param name="id">Id of the Broker.</param>
      /// <returns>Asynchronous list of Sales.</returns>
      /// <response code="200">OK - Sales data returned.</response>
      [HttpGet]
      [Route("/api/v{version:ApiVersion}/Brokers/{id}/[controller]")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async IAsyncEnumerable<GetSaleDTO> GetAll(string id)
      {
         await foreach (var svcRslt in _saleService.GetAllAsync(id)) yield return svcRslt.Value;
      }


      /// <summary>
      /// Obtain a given Sale.
      /// </summary>
      /// <param name="id">Id of the given Sale.</param>
      /// <returns>The Sale.</returns>
      /// <response code="200">OK - Sale data returned.</response>
      /// <response code="404">Not Found - no Sale for a given id.</response>
      [HttpGet("{id}", Name = "GetSaleById")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Get(string id)
      {
         var svcRslt = await _saleService.GetByIdAsync(id);
         if (!svcRslt.Success) return NotFound();
         return Ok(svcRslt.Value);
      }


      /// <summary>
      /// Add a new Sale.
      /// </summary>
      /// <param name="sale">A Sale to create.</param>
      /// <param name="version">Current API version.</param>
      /// <returns>A newly created Sale.</returns>
      /// <response code="201">Created - Sale successfully created (and returned).</response>
      /// <response code="400">Bad Request - empty or bad data posted.</response>
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> Create([FromBody] AddSaleDTO sale, ApiVersion version)
      {
         if (sale == null) return BadRequest();
         var svcRslt = await _saleService.AddAsync(sale);
         if (!svcRslt.Success) return BadRequest(svcRslt.Message);
         return CreatedAtRoute("GetSaleById", new { id = svcRslt.Value.Id, version = version.ToString() }, svcRslt.Value);
      }


      /// <summary>
      /// Replace an existing Sale.
      /// </summary>
      /// <param name="id">Id of the Sale to replace.</param>
      /// <param name="sale">The replacement Sale</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Sale successfully replaced.</response>
      /// <response code="400">Bad Request - empty or bad data posted, e.g. id mismatch.</response>
      /// <response code="404">Not Found - no Sale for a given id.</response>
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Update(string id, [FromBody] ReplaceSaleDTO sale)
      {
         if (sale == null) return BadRequest();
         if (sale.Id != id) return BadRequest(this.BadRequestDetails("Id mismatch detected.", $"Id values are immutable; an attempt to change '{id}' into '{sale.Id}' is invalid."));
         var svcRslt = await _saleService.ReplaceAsync(sale);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }


      /// <summary>
      /// Remove an existing Sale.
      /// </summary>
      /// <param name="id">ID of the Sale to remove.</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Sale successfully removed.</response>
      /// <response code="404">Not Found - no Sale for a given id.</response>
      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Delete(string id)
      {
         var svcRslt = await _saleService.DeleteAsync(id);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }
   }
}
