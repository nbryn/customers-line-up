using FluentValidation;

namespace CLup.Application.TimeSlots.Commands.GenerateTimeSlot;

public sealed class GenerateTimeSlotsValidator : AbstractValidator<GenerateTimeSlotsCommand>
{
    public GenerateTimeSlotsValidator()
    {
        RuleFor(command => command.BusinessId).NotEmpty();
        RuleFor(command => command.Start).NotEmpty();
    }
}
