using FluentValidation;

namespace CLup.Features.TimeSlots.Commands.Validation
{
    public class DeleteTimeSlotValidator : AbstractValidator<DeleteTimeSlotCommand>
    {
        public DeleteTimeSlotValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}