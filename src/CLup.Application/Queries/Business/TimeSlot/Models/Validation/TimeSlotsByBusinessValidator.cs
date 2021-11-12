using FluentValidation;

namespace CLup.Application.Queries.Business.TimeSlot.Models.Validation
{
    public class TimeSlotsByBusinessValidator : AbstractValidator<TimeSlotsByBusinessQuery>
    {
        public TimeSlotsByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}