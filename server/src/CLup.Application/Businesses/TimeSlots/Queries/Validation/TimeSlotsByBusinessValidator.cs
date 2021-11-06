using FluentValidation;

namespace CLup.Application.Businesses.TimeSlots.Queries.Validation
{
    public class TimeSlotsByBusinessValidator : AbstractValidator<TimeSlotsByBusinessQuery>
    {
        public TimeSlotsByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}