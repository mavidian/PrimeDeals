using Microsoft.AspNetCore.Mvc;

namespace PrimeDeals.API.Controllers
{
   internal static class ControllerHelpers
   {
      /// <summary>
      /// Problem details object to be returned with Http response 400 BadRequest
      /// </summary>
      /// <param name="title"></param>
      /// <param name="detail"></param>
      /// <returns></returns>
      internal static ProblemDetails BadRequestDetails(this ControllerBase _, string title, string detail) => new ProblemDetails
      {
         Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
         Title = title,
         Detail = detail
      };
   }
}
