using FluentValidation;
using PrimeDeals.Core.DTOs.Broker;

namespace PrimeDeals.Services.Validators
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
