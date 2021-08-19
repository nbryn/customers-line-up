using FluentValidation;

namespace CLup.Features.TimeSlots.Queries.Validation
{
    public class TimeSlotsByBusinessValidator : AbstractValidator<TimeSlotsByBusinessQuery>
    {
        public TimeSlotsByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}