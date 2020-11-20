using FluentValidation;
using PrimeDeals.Core.DTOs.Broker;

namespace PrimeDeals.Services.Validators.Broker
{
   public class ReplaceBrokerDTOValidator : AbstractValidator<ReplaceBrokerDTO>
   {
      public ReplaceBrokerDTOValidator()
      {
         //TODO: RuleFor(b => b.Id) - verify match
         RuleFor(b => b.Name).NotEmpty();
         RuleFor(x => x.Address).NotNull().SetValidator(new AddressValidator());
      }
   }
}
