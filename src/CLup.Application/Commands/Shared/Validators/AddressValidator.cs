using CLup.Domain.Shared.ValueObjects;
using FluentValidation;

namespace CLup.Application.Commands.Shared.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Zip).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
        }
    }
}