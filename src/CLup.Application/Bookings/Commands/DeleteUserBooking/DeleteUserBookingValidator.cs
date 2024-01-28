using CLup.Application.Bookings.Commands.DeleteUserBooking;
using FluentValidation;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class DeleteUserBookingValidator : AbstractValidator<DeleteUserBookingCommand>
{
    public DeleteUserBookingValidator()
    {
        RuleFor(command => command.BookingId).NotNull();
    }
}
