using FluentValidation;

namespace CLup.Domain.Shared.ValueObjects.Validation;

public class CoordsValidator : AbstractValidator<Coords>
{
    public CoordsValidator()
    {
        RuleFor(coords => coords.Latitude).InclusiveBetween(-90, 90);
        RuleFor(coords => coords.Longitude).InclusiveBetween(-180, 180);
    }
}
