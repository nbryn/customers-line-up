using FluentValidation;

namespace CLup.Domain.Shared.ValueObjects.Validation
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