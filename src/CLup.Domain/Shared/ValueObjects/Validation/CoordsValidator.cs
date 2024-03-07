namespace CLup.Domain.Shared.ValueObjects.Validation;

public class CoordsValidator : AbstractValidator<Coords>
{
    public CoordsValidator()
    {
        RuleFor(coords => coords.Latitude).NotEmpty().InclusiveBetween(-90, 90);
        RuleFor(coords => coords.Longitude).NotEmpty().InclusiveBetween(-180, 180);
    }
}
