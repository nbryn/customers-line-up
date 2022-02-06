using CLup.Domain.Businesses;
using CLup.Domain.Shared.ValueObjects;
using FluentValidation;

namespace CLup.Domain.Business
{
    public class BusinessValidator : AbstractValidator<Businesses.Business>
    {
        public BusinessValidator(
            IValidator<BusinessData> businessDataValidator,
            IValidator<Address> addressValidator,
            IValidator<Coords> coordsValidator,
            IValidator<TimeSpan> timeSpanValidator)
        {
            RuleFor(x => x.OwnerEmail).EmailAddress();
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.BusinessData).NotNull().SetValidator(businessDataValidator);
            RuleFor(x => x.Address).NotNull().SetValidator(addressValidator);
            RuleFor(x => x.Coords).NotNull().SetValidator(coordsValidator);
            RuleFor(x => x.BusinessHours).NotNull().SetValidator(timeSpanValidator);
        }
    }
}