using CLup.Domain.Shared.ValueObjects;
using FluentValidation;

namespace CLup.Application.Shared.Validators
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