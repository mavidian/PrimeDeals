using PrimeDeals.Core.Models.Base;
using static PrimeDeals.Core.Models.Broker;

namespace PrimeDeals.Core.DTOs.Broker
{
   public class AddBrokerDTO
   {
      public string Name { get; set; } = "Unknown Broker";
      public Address Address { get; set; }
      public string TaxIdNumber { get; set; } = string.Empty;
      public string State { get; set; } = BrokerState.BrokerActive.ToString();
   }
}
