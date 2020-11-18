using AutoMapper;
using MediatR;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Queries.Brokers
{
   public class GetAllBrokersQuery : GetAllQuery<GetBrokerDTO>  { }


   public class GetAllBrokersQueryHnadler : GetAllQueryHandler<Broker, GetBrokerDTO>, IRequestHandler<GetAllBrokersQuery, IAsyncEnumerable<IServiceResult<GetBrokerDTO>>>
   {
      public GetAllBrokersQueryHnadler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.BrokerRepo;
      }

      public Task<IAsyncEnumerable<IServiceResult<GetBrokerDTO>>> Handle(GetAllBrokersQuery request, CancellationToken cancellationToken) => base.Handle(request, cancellationToken);
   }
}
