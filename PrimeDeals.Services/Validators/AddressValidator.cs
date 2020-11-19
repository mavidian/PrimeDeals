using FluentValidation;
using PrimeDeals.Core.Models.Base;
using System.Collections.Generic;

namespace PrimeDeals.Services.Validators
{
   class AddressValidator : AbstractValidator<Address>
   {
      public AddressValidator()
      {
         RuleFor(a => a.State).Must(s => new List<string> { "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY" }.Contains(s.ToUpper()))
                              .WithMessage("Must be a valid US State abbreviation.");
         RuleFor(a => a.Zip).Matches("\\d{5}")
                            .WithMessage("Must contain 5 digits.");
      }
   }
}
