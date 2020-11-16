using PrimeDeals.Core.Interfaces.Services;

namespace PrimeDeals.Services.Services
{
   public class ServiceResult<TVal> : IServiceResult<TVal>
   {
      public TVal Value { get; set; }
      public bool Success { get; set; } = true;
      public string Message { get; set; }
   }

   public class ServiceResult : IServiceResult
   {
      public bool Success { get; set; } = true;
      public string Message { get; set; }
   }
}
