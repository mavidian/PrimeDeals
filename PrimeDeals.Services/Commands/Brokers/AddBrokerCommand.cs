using AutoMapper;
using MediatR;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;
using PrimeDeals.Services.Services;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeDeals.Services.Commands.Brokers
{
   public class AddBrokerCommand : AddCommand<AddBrokerDTO, GetBrokerDTO> { }

   public class AddBrokerCommandHandler : AddCommandHandler<Broker, AddBrokerDTO, GetBrokerDTO>, IRequestHandler<AddBrokerCommand, ServiceResult<GetBrokerDTO>>
   {
      public AddBrokerCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.BrokerRepo;
      }

      public Task<ServiceResult<GetBrokerDTO>> Handle(AddBrokerCommand request, CancellationToken cancellationToken) => base.Handle(request, cancellationToken);
   }
}
