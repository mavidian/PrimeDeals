using AutoMapper;
using PrimeDeals.Core.DTOs.Policy;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Core.Models;

namespace PrimeDeals.Services.Services
{
   public class PolicyService : Service<Policy,GetPolicyDTO,AddPolicyDTO,ReplacePolicyDTO>, IPolicyService
   {
      public PolicyService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.PolicyRepo;
      }

   }
}
