using PrimeDeals.Core.Models.Base;

namespace PrimeDeals.Core.DTOs.Broker
{
   public class ReplaceBrokerDTO
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public Address Address { get; set; }
      public string TaxIdNumber { get; set; }
      public string State { get; set; }
   }
}
