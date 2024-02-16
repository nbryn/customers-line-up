using FluentValidation;

namespace CLup.Domain.Businesses.ValueObjects.Validation;

public class BusinessDataValidator : AbstractValidator<BusinessData>
{
    public BusinessDataValidator()
    {
        RuleFor(data => data.Name).NotEmpty().MaximumLength(50);
        RuleFor(data => data.Capacity).NotEmpty().GreaterThan(0);
        RuleFor(data => data.TimeSlotLengthInMinutes).NotEmpty().GreaterThanOrEqualTo(5);
    }
}
