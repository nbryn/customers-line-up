using FluentValidation;

using CLup.Businesses.DTO;

namespace CLup.Businesses
{
    public class BusinessesRequestValidator : AbstractValidator<BusinessRequest>
    {
        public BusinessesRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OwnerEmail).EmailAddress();
            RuleFor(x => x.Zip).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Opens).NotEmpty();
            RuleFor(x => x.Closes).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty();
            RuleFor(x => x.TimeSlotLength).NotEmpty();
            RuleFor(x => x.Type).IsEnumName(typeof(BusinessType));
        }
    }
}