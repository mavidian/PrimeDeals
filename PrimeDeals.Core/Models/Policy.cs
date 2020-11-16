using PrimeDeals.Core.Interfaces.Models;
using PrimeDeals.Core.Models.Base;
using static PrimeDeals.Core.Models.Policy;

namespace PrimeDeals.Core.Models
{
   public class Policy : SmEntity<PolicyState, PolicyTrigger>, IEntity
   {
      public enum PolicyState
      {
         PolicyReady,            // 0
         InUnderwriting,         // 1
         PolicyApproved,         // 2
         Failed,                 // 3
         Complete,               // 3
         EnrollmentInProgress    // 4
      }

      public enum PolicyTrigger
      {
         BrokerSubmitsGroupApplication,  // 0
         BrokerAmmendsGroupApplication,  // 1
         PlanRejectsPolicy,              // 2
         PlanApprovesPolicy,             // 3
         GroupRejectsPolicy,             // 4
         GroupApprovesPolicy,            // 5
         PlanSetsUpGroup                 // 6
      }

      public string Id { get; set; }
      public string ParentId { get; set; }  //Sale ID
      public string ProductName { get; set; }


      public Policy() : base() => ConfigureSM();


      private void ConfigureSM()
      {
         SM.Configure(PolicyState.PolicyReady)
            .Permit(PolicyTrigger.BrokerSubmitsGroupApplication, PolicyState.InUnderwriting);

         SM.Configure(PolicyState.InUnderwriting)
            .Permit(PolicyTrigger.PlanRejectsPolicy, PolicyState.Failed)
            .PermitReentry(PolicyTrigger.BrokerAmmendsGroupApplication)
            .Permit(PolicyTrigger.PlanApprovesPolicy, PolicyState.PolicyApproved);

         SM.Configure(PolicyState.PolicyApproved)
            .Permit(PolicyTrigger.GroupRejectsPolicy, PolicyState.Failed)
            .Permit(PolicyTrigger.GroupApprovesPolicy, PolicyState.Complete);

         SM.Configure(PolicyState.Complete)
            .Permit(PolicyTrigger.PlanSetsUpGroup, PolicyState.EnrollmentInProgress);
      }

   }
}
