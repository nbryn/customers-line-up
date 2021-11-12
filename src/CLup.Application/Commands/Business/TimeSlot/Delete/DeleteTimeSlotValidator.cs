using FluentValidation;

namespace CLup.Application.Commands.Business.TimeSlot.Delete
{
    public class DeleteTimeSlotValidator : AbstractValidator<DeleteTimeSlotCommand>
    {
        public DeleteTimeSlotValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}