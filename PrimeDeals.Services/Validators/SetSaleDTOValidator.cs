using FluentValidation;
using PrimeDeals.Core.DTOs.Sale;
using PrimeDeals.Core.Interfaces.Repositories;

namespace PrimeDeals.Services.Validators
{
   public class SetSaleDTOValidator : AbstractValidator<SetSaleDTO>
   {
      public SetSaleDTOValidator(IUnitOfWork uow)
      {
         //Note that to enforce Broker-Sale integrity, unit of work should only encompass a single Sale add/update transaction (which is not the case in this demo)
         RuleFor(s => s.BrokerId).Must(id => uow.BrokerRepo.ContainsId(id)).WithMessage("'{PropertyName}' must represent a valid Broker.");
         RuleFor(s => s.GroupName).NotEmpty();
         RuleFor(s => s.GroupSize).GreaterThan(9).LessThanOrEqualTo(500);
      }
   }
}
