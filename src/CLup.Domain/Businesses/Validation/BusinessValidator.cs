using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using FluentValidation;

namespace CLup.Domain.Businesses.Validation;

public class BusinessValidator : AbstractValidator<Business>
{
    public BusinessValidator(
        IValidator<BusinessData> businessDataValidator,
        IValidator<Address> addressValidator,
        IValidator<Coords> coordsValidator,
        IValidator<TimeInterval> timeSpanValidator)
    {
        RuleFor(business => business.OwnerId).NotEmpty();
        RuleFor(business => business.Type).NotEmpty().IsInEnum();
        RuleFor(business => business.BusinessData).NotEmpty().SetValidator(businessDataValidator);
        RuleFor(business => business.Address).NotEmpty().SetValidator(addressValidator);
        RuleFor(business => business.Coords).NotEmpty().SetValidator(coordsValidator);
        RuleFor(business => business.BusinessHours).NotEmpty().SetValidator(timeSpanValidator);
    }
}
