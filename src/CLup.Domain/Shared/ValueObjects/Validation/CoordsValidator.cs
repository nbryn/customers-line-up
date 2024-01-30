using FluentValidation;

namespace CLup.Domain.Shared.ValueObjects.Validation;

public class CoordsValidator : AbstractValidator<Coords>
{
    public CoordsValidator()
    {
        RuleFor(request => request.Latitude).InclusiveBetween(-90, 90);
        RuleFor(request => request.Longitude).InclusiveBetween(-180, 180);
    }
}
