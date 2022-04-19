using FluentValidation;

namespace CLup.Application.Businesses.TimeSlots
{
    public class TimeSlotValidator : AbstractValidator<Domain.Businesses.TimeSlots.TimeSlot>
    {
        public TimeSlotValidator()
        {
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}