using static PrimeDeals.Core.Models.Sale;

namespace PrimeDeals.Core.DTOs.Sale
{
   /// <summary>
   /// Applicable to POST and PUT actions
   /// </summary
   public class SetSaleDTO
   {
      public string BrokerId { get; set; }
      public string GroupName { get; set; } = "Unknown Group";
      public int GroupSize { get; set; } = 10;
      public string State { get; set; } = SaleState.NewSale.ToString();
   }
}
