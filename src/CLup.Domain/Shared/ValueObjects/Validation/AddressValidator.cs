namespace CLup.Domain.Shared.ValueObjects.Validation;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator(IValidator<Coords> coordsValidator)
    {
        RuleFor(address => address.Street).NotEmpty();
        RuleFor(address => address.City).NotEmpty();
        RuleFor(address => address.Zip).NotEmpty().Must(zip => zip.ToString().Length == 4);
        RuleFor(address => address.Coords).NotEmpty().SetValidator(coordsValidator);
    }
}
