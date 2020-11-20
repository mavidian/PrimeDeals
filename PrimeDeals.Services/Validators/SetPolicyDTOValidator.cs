using FluentValidation;
using PrimeDeals.Core.DTOs.Policy;
using PrimeDeals.Core.Interfaces.Repositories;

namespace PrimeDeals.Services.Validators
{
   public class SetPolicyDTOValidator : AbstractValidator<SetPolicyDTO>
   {
      public SetPolicyDTOValidator(IUnitOfWork uow)
      {
         RuleFor(p => p.SaleId).Must(id => uow.SaleRepo.ContainsId(id)).WithMessage("'{PropertyName}' must represent a valid Sale.");
         RuleFor(p => p.ProductName).NotEmpty();
      }
   }
}
