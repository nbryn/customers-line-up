using FluentValidation;

namespace CLup.Application.Bookings.Commands.BusinessDeleteBooking;

public sealed class DeleteBusinessBookingValidator : AbstractValidator<DeleteBusinessBookingCommand>
{
    public DeleteBusinessBookingValidator()
    {
        RuleFor(command => command.BookingId).NotNull();
        RuleFor(command => command.BusinessId).NotNull();
    }
}
