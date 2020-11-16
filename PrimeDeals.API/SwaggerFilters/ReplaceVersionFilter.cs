using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace PrimeDeals.API.SwaggerFilters
{
   /// <summary>
   /// Document filter to substitute version tokens (v{version}) in path displayed by Swagger UI
   /// </summary>
   public class ReplaceVersionFilter : IDocumentFilter
   {
      public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
      {
         var updatedPaths = new OpenApiPaths();
         swaggerDoc.Paths.ToList().ForEach(p => updatedPaths.Add(p.Key.Replace("v{version}", swaggerDoc.Info.Version), p.Value));
         swaggerDoc.Paths = updatedPaths;
      }
   }
}
