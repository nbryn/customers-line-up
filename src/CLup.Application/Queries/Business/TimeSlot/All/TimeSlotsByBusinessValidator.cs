using FluentValidation;

namespace CLup.Application.Queries.Business.TimeSlot.All
{
    public class TimeSlotsByBusinessValidator : AbstractValidator<TimeSlotsByBusinessQuery>
    {
        public TimeSlotsByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}