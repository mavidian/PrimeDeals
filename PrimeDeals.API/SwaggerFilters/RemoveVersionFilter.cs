using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace PrimeDeals.API.SwaggerFilters
{
   /// <summary>
   /// Operation filter to remove the parameter texbox (named version) from Swagger UI
   /// </summary>
   public class RemoveVersionFilter : IOperationFilter
   {
      public void Apply(OpenApiOperation operation, OperationFilterContext context)
      {
         var versionParameter = operation.Parameters.Single(p => p.Name == "version");
         operation.Parameters.Remove(versionParameter);
      }
   }
}
