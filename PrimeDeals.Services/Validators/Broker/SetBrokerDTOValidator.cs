using FluentValidation;
using PrimeDeals.Core.DTOs.Broker;
using PrimeDeals.Services.Validators;

namespace PrimeDeals.Services.Validatorrs.Broker
{
   public class SetBrokerDTOValidator : AbstractValidator<SetBrokerDTO>
   {
      public SetBrokerDTOValidator()
      {
         RuleFor(b => b.Name).NotEmpty();
         RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
      }
   }
}
