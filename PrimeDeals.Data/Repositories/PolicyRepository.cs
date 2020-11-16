using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;
using System.Collections.Generic;

namespace PrimeDeals.Data.Repositories
{
   public class PolicyRepository : Repository<Policy>, IPolicyRepository
   {
      public PolicyRepository(Dictionary<string, Policy> policies) : base(policies) { }

      protected override string NewId() => NewId(IPolicyRepository.IdPrefix, IPolicyRepository.IdLength);
   }
}
