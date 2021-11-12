using FluentValidation;

namespace CLup.Application.Queries.Business.TimeSlot.Available
{
    public class AvailableTimeSlotsByBusinessValidator : AbstractValidator<AvailableTimeSlotsByBusinessQuery>
    {
        public AvailableTimeSlotsByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();
        }
    }
}