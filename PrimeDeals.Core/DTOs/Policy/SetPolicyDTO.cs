using static PrimeDeals.Core.Models.Policy;

namespace PrimeDeals.Core.DTOs.Policy
{
   /// <summary>
   /// Applicable to POST and PUT actions
   /// </summary
   public class SetPolicyDTO
   {
      public string SaleId { get; set; }
      public string ProductName { get; set; } = "Unknown Product";
      public string State { get; set; } = PolicyState.PolicyReady.ToString();
   }
}
