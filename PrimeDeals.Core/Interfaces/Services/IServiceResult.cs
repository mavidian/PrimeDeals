namespace PrimeDeals.Core.Interfaces.Services
{

   /// <summary>
   /// Wraps service response result data.
   /// <typeparam name="TResp">Type of service response.</typeparam>
   public interface IServiceResult<TResp> : IServiceResult
   {
      /// <summary>
      /// Data returned by the service.
      /// </summary>
      TResp Value { get; set; }
   }

   /// <summary>
   /// Result returned by all services.
   /// </summary>
   public interface IServiceResult
   {
      /// <summary>
      /// true means successful outcome; false means failure.
      /// </summary>
      bool Success { get; set; }
      /// <summary>
      /// Message associated with service outcome, e.g. exception message.
      /// </summary>
      string Message { get; set; }
   }
}