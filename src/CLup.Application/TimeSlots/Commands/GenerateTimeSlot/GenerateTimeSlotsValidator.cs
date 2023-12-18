using FluentValidation;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot
{
    public class GenerateTimeSlotsValidator : AbstractValidator<GenerateTimeSlotsCommand>
    {
        public GenerateTimeSlotsValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
        }
    }
}