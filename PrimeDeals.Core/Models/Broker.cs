using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Models.Base;
using static PrimeDeals.Core.Models.Broker;

namespace PrimeDeals.Core.Models
{
   public class Broker : SmEntity<BrokerState, BrokerTrigger>, IEntity
   {
      public enum BrokerState
      {
         BrokerInactive,         // 0
         ApplicationAccepted,    // 1
         ApplicationApproved,    // 2
         BrokerCertified,        // 3
         BrokerActive            // 4
      }

      public enum BrokerTrigger
      {
         BrokerSubmitsApplication,  // 0
         BrokerAmendsApplication,   // 1
         PlanRejectsApplication,    // 2
         PlanApprovesApplication,   // 3
         BrokerSignsContract,       // 4
         SalesTimeframeCommences,   // 5
         SalesTimeframeExpires      // 6
      }

      public string Id { get; set; }
      public string ParentId { get; set; }  //always null as Broker is at top level

      public string Name { get; set; } //broker name
      public Address Address { get; set; }
      public string TaxIdNumber { get; set; }


      public Broker() : base() => ConfigureSM();


      private void ConfigureSM()
      {
         SM.Configure(BrokerState.BrokerInactive)
            .Permit(BrokerTrigger.BrokerSubmitsApplication, BrokerState.ApplicationAccepted);

         SM.Configure(BrokerState.ApplicationAccepted)
            .Permit(BrokerTrigger.PlanRejectsApplication, BrokerState.BrokerInactive)
            .PermitReentry(BrokerTrigger.BrokerAmendsApplication)
            .Permit(BrokerTrigger.PlanApprovesApplication, BrokerState.ApplicationApproved);

         SM.Configure(BrokerState.ApplicationApproved)
            .Permit(BrokerTrigger.BrokerSignsContract, BrokerState.BrokerCertified);

         SM.Configure(BrokerState.BrokerCertified)
            .OnEntry(() => { if (true) SM.Fire(BrokerTrigger.SalesTimeframeCommences); })  //simulate auto-advance
            .Permit(BrokerTrigger.SalesTimeframeCommences, BrokerState.BrokerActive);


         SM.Configure(BrokerState.BrokerActive)
            .Permit(BrokerTrigger.SalesTimeframeExpires, BrokerState.BrokerInactive);
      }

   }
}