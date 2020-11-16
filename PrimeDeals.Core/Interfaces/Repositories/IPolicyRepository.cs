using PrimeDeals.Core.Models;

namespace PrimeDeals.Core.Interfaces.Repositories
{
   public interface IPolicyRepository : IRepository<Policy>
   {
      new static string StoragePrefix => "Policy_";
      new static string IdPrefix => "P";
      new static int IdLength => 5;
   }
}
