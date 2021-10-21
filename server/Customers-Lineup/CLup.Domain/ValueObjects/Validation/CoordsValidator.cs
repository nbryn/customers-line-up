using FluentValidation;

namespace CLup.Domain.ValueObjects.Validation
{
    public class CoordsValidator : AbstractValidator<Coords>
    {
        public CoordsValidator()
        {
            RuleFor(x => x.Latitude).NotEmpty().GreaterThan(-86).LessThan(86);
            RuleFor(x => x.Longitude).NotEmpty().GreaterThan(-181).LessThan(181);
        }
    }
}