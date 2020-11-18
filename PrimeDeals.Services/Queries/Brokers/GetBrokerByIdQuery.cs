using AutoMapper;
using MediatR;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Queries.Brokers
{
   public class GetBrokerByIdQuery : GetByIdQuery<GetBrokerDTO> { }

   public class GetBrokerByIdQueryHandler : GetByIdQueryHandler<Broker, GetBrokerDTO>, IRequestHandler<GetBrokerByIdQuery, IServiceResult<GetBrokerDTO>>
   {
      public GetBrokerByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.BrokerRepo;
      }

      public Task<IServiceResult<GetBrokerDTO>> Handle(GetBrokerByIdQuery request, CancellationToken cancellationToken) => base.Handle(request, cancellationToken);

   }
}
