using FluentValidation;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotValidator : AbstractValidator<DeleteTimeSlotCommand>
{
    public DeleteTimeSlotValidator()
    {
        RuleFor(command => command.BusinessId).NotEmpty();
        RuleFor(command => command.TimeSlotId).NotEmpty();
    }
}
