using AutoMapper;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Models;

namespace PrimeDeals.Services.Queries.Brokers
{
   public class GetBrokerByIdQuery : GetByIdQuery<GetBrokerDTO> { }

   public class GetBrokerByIdHandler : GetByIdHandler<Broker, GetBrokerDTO>
   {

      public GetBrokerByIdHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.BrokerRepo;
      }

   }
}
