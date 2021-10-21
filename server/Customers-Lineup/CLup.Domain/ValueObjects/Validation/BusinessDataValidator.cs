using FluentValidation;

namespace CLup.Domain.ValueObjects.Validation
{
    public class BusinessDataValidator : AbstractValidator<BusinessData>
    {
        public BusinessDataValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Capacity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.TimeSlotLength).NotEmpty().GreaterThan(0);
        }
    }
}