using PrimeDeals.Core.Models;

namespace PrimeDeals.Core.Interfaces.Repositories
{
   public interface ISaleRepository : IRepository<Sale>
   {
      new static string StoragePrefix => "Sale_";
      new static string IdPrefix => "S";
      new static int IdLength => 4;
   }
}
