using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using FluentValidation;

namespace CLup.Domain.Businesses.Validation;

public class BusinessValidator : AbstractValidator<Domain.Businesses.Business>
{
    public BusinessValidator(
        IValidator<BusinessData> businessDataValidator,
        IValidator<Address> addressValidator,
        IValidator<Coords> coordsValidator,
        IValidator<Interval> timeSpanValidator)
    {
        RuleFor(business => business.OwnerId).NotNull();
        RuleFor(business => business.Type).NotEmpty().IsInEnum();
        RuleFor(business => business.BusinessData).NotNull().SetValidator(businessDataValidator);
        RuleFor(business => business.Address).NotNull().SetValidator(addressValidator);
        RuleFor(business => business.Coords).NotNull().SetValidator(coordsValidator);
        RuleFor(business => business.BusinessHours).NotNull().SetValidator(timeSpanValidator);
    }
}
