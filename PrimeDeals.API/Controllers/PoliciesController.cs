using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeDeals.Core.DTOs.Policy;
using PrimeDeals.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimeDeals.API.Controllers
{
   [ApiController]
   [Route("api/v{version:ApiVersion}/[controller]")]
   [SwaggerTag("Each Policy belongs to a Sale; there are no child entities for a Policy.")]
   [ApiVersion("1.0")]
   public class PoliciesController : ControllerBase
   {
      private readonly IPolicyService _policyService;

      public PoliciesController(IPolicyService policyService)
      {
         _policyService = policyService;
      }


      /// <summary>
      /// Obtain all Policies.
      /// </summary>
      /// <returns>Asynchronous list of all Policies.</returns>
      /// <response code="200">OK - Policies data returned.</response>
      [HttpGet]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async IAsyncEnumerable<GetPolicyDTO> GetAll()
      {
         await foreach (var svcRslt in _policyService.GetAllAsync()) yield return svcRslt.Value;
      }


      /// <summary>
      /// Obtain all Policies for a given Sale.
      /// </summary>
      /// <param name="id">Id of the Sale.</param>
      /// <returns>Asynchronous list of Policies.</returns>
      /// <response code="200">OK - Policies data returned.</response>
      [HttpGet]
      [Route("/api/v{version:ApiVersion}/Sales/{id}/[controller]")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      public async IAsyncEnumerable<GetPolicyDTO> GetAll(string id)
      {
         await foreach (var svcRslt in _policyService.GetAllAsync(id)) yield return svcRslt.Value;
      }


      /// <summary>
      /// Obtain a given Policy.
      /// </summary>
      /// <param name="id">Id of the given Policy.</param>
      /// <returns>The Policy.</returns>
      /// <response code="200">OK - Policy data returned.</response>
      /// <response code="404">Not Found - no Policy for a given id.</response>
      [HttpGet("{id}", Name = "GetPolicyById")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Get(string id)
      {
         var svcRslt = await _policyService.GetByIdAsync(id);
         if (!svcRslt.Success) return NotFound();
         return Ok(svcRslt.Value);
      }


      /// <summary>
      /// Add a new Policy.
      /// </summary>
      /// <param name="policy">A Policy to create.</param>
      /// <param name="version">Current API version.</param>
      /// <returns>A newly created Policy.</returns>
      /// <response code="201">Created - Policy successfully created (and returned).</response>
      /// <response code="400">Bad Request - empty or bad data posted.</response>
      [HttpPost]
      [ProducesResponseType(StatusCodes.Status201Created)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      public async Task<IActionResult> Create([FromBody] AddPolicyDTO policy, ApiVersion version)
      {
         if (policy == null) return BadRequest();
         var svcRslt = await _policyService.AddAsync(policy);
         if (!svcRslt.Success) return BadRequest(svcRslt.Message);
         return CreatedAtRoute("GetPolicyById", new { id = svcRslt.Value.Id, version = version.ToString() }, svcRslt.Value);
      }


      /// <summary>
      /// Replace an existing Policy.
      /// </summary>
      /// <param name="id">Id of the Policy to replace.</param>
      /// <param name="policy">The replacement Policy</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Policy successfully replaced.</response>
      /// <response code="400">Bad Request - empty or bad data posted, e.g. id mismatch.</response>
      /// <response code="404">Not Found - no Policy for a given id.</response>
      [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Update(string id, [FromBody] ReplacePolicyDTO policy)
      {
         if (policy == null) return BadRequest();
         if (policy.Id != id) return BadRequest(this.BadRequestDetails("Id mismatch detected.", $"Id values are immutable; an attempt to change '{id}' into '{policy.Id}' is invalid."));
         var svcRslt = await _policyService.ReplaceAsync(policy);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }


      /// <summary>
      /// Remove an existing Policy.
      /// </summary>
      /// <param name="id">ID of the Policy to remove.</param>
      /// <returns>An empty response.</returns>
      /// <response code="204">No Content - Policy successfully removed.</response>
      /// <response code="404">Not Found - no Policy for a given id.</response>
      [HttpDelete("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
      [ProducesResponseType(StatusCodes.Status404NotFound)]
      public async Task<IActionResult> Delete(string id)
      {
         var svcRslt = await _policyService.DeleteAsync(id);
         if (!svcRslt.Success) return NotFound();
         return NoContent();
      }
   }
}
