using CLup.Domain.Businesses;
using FluentValidation;

namespace CLup.Application.Businesses
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