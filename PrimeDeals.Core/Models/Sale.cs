using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Models.Base;
using static PrimeDeals.Core.Models.Sale;

namespace PrimeDeals.Core.Models
{
   public class Sale : SmEntity<SaleState, SaleTrigger>, IEntity
   {
      public enum SaleState
      {
         NewSale,           // 0
         ProductsOffered,   // 1
         ProductsSelected,  // 2
         RosterProvided,    // 3
         SaleReady,         // 4
         SaleFailed,        // 5
         SaleComplete       // 6
      }

      public enum SaleTrigger
      {                             // 0
         GroupSignsUp,              // 1
         GroupSelectsProducts,      // 2
         GroupProvidesRoster,       // 3
         AllPoliciesFailed,         // 4
         OneOrMorePoliciesComplete  // 5
      }

      public string Id { get; set; }
      public string ParentId { get; set; }  //Broker ID
      public string GroupName { get; set; }
      public int GroupSize { get; set; }

      public Sale() : base() => ConfigureSM();


      private void ConfigureSM()
      {
         SM.Configure(SaleState.NewSale)
            .Permit(SaleTrigger.GroupSignsUp, SaleState.ProductsOffered);

         SM.Configure(SaleState.ProductsOffered)
            .Permit(SaleTrigger.GroupSelectsProducts, SaleState.ProductsSelected)
            .Permit(SaleTrigger.GroupProvidesRoster, SaleState.RosterProvided);

         SM.Configure(SaleState.ProductsSelected)
            .Permit(SaleTrigger.GroupProvidesRoster, SaleState.SaleReady);

         SM.Configure(SaleState.RosterProvided)
            .Permit(SaleTrigger.GroupSelectsProducts, SaleState.SaleReady);

         SM.Configure(SaleState.SaleReady)
            .Permit(SaleTrigger.AllPoliciesFailed, SaleState.SaleFailed)
            .Permit(SaleTrigger.OneOrMorePoliciesComplete, SaleState.SaleComplete);
      }

   }
}
