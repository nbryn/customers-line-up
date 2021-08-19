using FluentValidation;

using CLup.Domain;

namespace CLup.Features.Businesses.Commands.Validation
{
    public class CreateBusinessValidator : AbstractValidator<CreateBusinessCommand>
    {
        public CreateBusinessValidator()
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