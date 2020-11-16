using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;
using System.Collections.Generic;

namespace PrimeDeals.Data.Repositories
{
   public class SaleRepository : Repository<Sale>, ISaleRepository
   {
      public SaleRepository(Dictionary<string, Sale> sales) : base(sales) { }

      protected override string NewId() => NewId(ISaleRepository.IdPrefix, ISaleRepository.IdLength);
   }
}
