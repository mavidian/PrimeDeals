using System;
using System.Threading.Tasks;

namespace PrimeDeals.Core.Interfaces.Repositories
{
   public interface IUnitOfWork : IDisposable
   {
      IBrokerRepository BrokerRepo { get; }
      ISaleRepository SaleRepo { get; }
      IPolicyRepository PolicyRepo { get; }
      Task<int> CommitAsync();
   }
}
