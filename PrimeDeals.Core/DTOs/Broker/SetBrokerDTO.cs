using PrimeDeals.Core.Models.Base;
using static PrimeDeals.Core.Models.Broker;

namespace PrimeDeals.Core.DTOs.Broker
{
   /// <summary>
   /// Applicable to POST and PUT actions
   /// </summary>
   public class SetBrokerDTO
   {
      public string Name { get; set; } = "Unknown Broker";
      public Address Address { get; set; } = new Address { Street = "123 Main St", City = "Anytown", State = "NJ", Zip = "00000" };
      public string TaxIdNumber { get; set; } = string.Empty;
      public string State { get; set; } = BrokerState.BrokerActive.ToString();
   }
}
