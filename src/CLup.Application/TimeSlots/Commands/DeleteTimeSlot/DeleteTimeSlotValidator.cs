using FluentValidation;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot
{
    public class DeleteTimeSlotValidator : AbstractValidator<DeleteTimeSlotCommand>
    {
        public DeleteTimeSlotValidator()
        {
            RuleFor(x => x.TimeSlotId).NotEmpty();
        }
    }
}