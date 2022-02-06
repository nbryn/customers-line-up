using FluentValidation;

namespace CLup.Domain.Business.TimeSlot
{
    public class TimeSlotValidator : AbstractValidator<Businesses.TimeSlots.TimeSlot>
    {
        public TimeSlotValidator()
        {
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}