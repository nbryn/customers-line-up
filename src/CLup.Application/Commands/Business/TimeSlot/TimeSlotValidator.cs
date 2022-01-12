using FluentValidation;

namespace CLup.Domain.Business.TimeSlot
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