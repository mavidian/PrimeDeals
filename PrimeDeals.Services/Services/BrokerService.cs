using AutoMapper;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Core.Models;
using System;
using System.Collections.Generic;

namespace PrimeDeals.Services.Services
{
   public class BrokerService : Service<Broker,GetBrokerDTO,AddBrokerDTO,ReplaceBrokerDTO>, IBrokerService
   {
      public BrokerService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.BrokerRepo;
      }

      public override IAsyncEnumerable<IServiceResult<GetBrokerDTO>> GetAllAsync(string parentId)
      {
         throw new InvalidOperationException("There is no parent entity for brokers.");
      }
   }
}
