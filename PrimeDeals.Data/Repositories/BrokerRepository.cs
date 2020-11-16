using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;
using System;
using System.Collections.Generic;

namespace PrimeDeals.Data.Repositories
{
   public class BrokerRepository : Repository<Broker>, IBrokerRepository
   {
      public BrokerRepository(Dictionary<string, Broker> brokers) : base(brokers) { }

      public override IAsyncEnumerable<Broker> GetAllAsync(string parentId)
      {
         throw new InvalidOperationException("There is no parent entity for brokers.");
      }

      protected override string NewId() => NewId(IBrokerRepository.IdPrefix, IBrokerRepository.IdLength);
   }
}
