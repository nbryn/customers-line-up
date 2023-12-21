using FluentValidation;

namespace CLup.Application.Bookings.Commands.CreateBooking;

public sealed class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingValidator()
    {
        RuleFor(command => command.TimeSlotId).NotNull();
        RuleFor(command => command.BusinessId).NotNull();
    }
}
