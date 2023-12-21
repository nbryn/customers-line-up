using FluentValidation;

namespace CLup.Domain.TimeSlots.Validation;

public class TimeSlotValidator : AbstractValidator<TimeSlot>
{
    public TimeSlotValidator()
    {
        RuleFor(timeSlot => timeSlot.BusinessId).NotEmpty();
        RuleFor(timeSlot => timeSlot.Start).NotEmpty();
        RuleFor(timeSlot => timeSlot.End).NotEmpty();
        RuleFor(timeSlot => timeSlot.Start < timeSlot.End);
    }
}
