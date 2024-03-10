using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Businesses.Validation;

public class BusinessValidator : AbstractValidator<Business>
{
    public BusinessValidator(
        IValidator<BusinessData> businessDataValidator,
        IValidator<Address> addressValidator,
        IValidator<TimeInterval> timeSpanValidator)
    {
        RuleFor(business => business.OwnerId).NotEmpty();
        RuleFor(business => business.Type).NotEmpty().IsInEnum();
        RuleFor(business => business.BusinessData).NotEmpty().SetValidator(businessDataValidator);
        RuleFor(business => business.Address).NotEmpty().SetValidator(addressValidator);
        RuleFor(business => business.BusinessHours).NotEmpty().SetValidator(timeSpanValidator);

        RuleFor(business => business.BusinessHours.Start.Minute).Must(minute => minute % 5 == 0);
        RuleFor(business => business.BusinessHours.End.Minute).Must(minute => minute % 5 == 0);

        RuleFor(business => business)
            .Must(business => business.BusinessData.TimeSlotLengthInMinutes <= (business.BusinessHours.End - business.BusinessHours.Start).TotalMinutes)
            .WithMessage(BusinessErrors.TimeSlotLengthExceedsOpeningHours.Message);

        RuleFor(business => business.BusinessData.TimeSlotLengthInMinutes)
            .Must(timeSlotLength => timeSlotLength % 5 == 0)
            .WithMessage(BusinessErrors.InvalidTimeSlotLength.Message);
    }
}
