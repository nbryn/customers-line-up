using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.TimeSlots.Validation;

public class TimeSlotValidator : AbstractValidator<TimeSlot>
{
    public TimeSlotValidator(IValidator<TimeInterval> timeSpanValidator)
    {
        RuleFor(timeSlot => timeSlot.BusinessId).NotEmpty();
        RuleFor(timeSlot => timeSlot.Capacity).NotEmpty().GreaterThan(0);
        RuleFor(timeSlot => timeSlot.Date).NotEmpty();
        RuleFor(timeSlot => timeSlot.TimeInterval).NotEmpty().SetValidator(timeSpanValidator);
    }
}
