using FluentValidation;

namespace CLup.Application.Businesses.TimeSlots.Commands.Validation
{
    public class DeleteTimeSlotValidator : AbstractValidator<DeleteTimeSlotCommand>
    {
        public DeleteTimeSlotValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}