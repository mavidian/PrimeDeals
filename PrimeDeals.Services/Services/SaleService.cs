using AutoMapper;
using PrimeDeals.Core.DTOs.Sale;
using PrimeDeals.Core.Interfaces.Repositories;
using PrimeDeals.Core.Interfaces.Services;
using PrimeDeals.Core.Models;

namespace PrimeDeals.Services.Services
{
   public class SaleService : Service<Sale,GetSaleDTO,SetSaleDTO>, ISaleService
   {
      public SaleService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper)
      {
         _repository = unitOfWork.SaleRepo;
      }

   }
}
