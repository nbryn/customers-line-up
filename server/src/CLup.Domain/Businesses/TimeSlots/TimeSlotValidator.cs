using FluentValidation;

namespace CLup.Domain.Businesses.TimeSlots
{
    public class TimeSlotValidator : AbstractValidator<TimeSlot>
    {
        public TimeSlotValidator()
        {
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}