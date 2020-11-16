using static PrimeDeals.Core.Models.Policy;

namespace PrimeDeals.Core.DTOs.Policy
{
   public class AddPolicyDTO
   {
      public string SaleId { get; set; }
      public string ProductName { get; set; } = "Unknown Product";
      public string State { get; set; } = PolicyState.PolicyReady.ToString();
   }
}
