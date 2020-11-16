using PrimeDeals.Core.Models;

namespace PrimeDeals.Core.Interfaces.Repositories
{
   public interface IBrokerRepository : IRepository<Broker>
   {
      new static string StoragePrefix => "Broker_";
      new static string IdPrefix => "B";
      new static int IdLength => 3;
   }
}

