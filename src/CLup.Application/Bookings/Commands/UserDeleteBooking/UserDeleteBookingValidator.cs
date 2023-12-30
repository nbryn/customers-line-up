using FluentValidation;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class UserDeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
{
    public UserDeleteBookingValidator()
    {
        RuleFor(command => command.BookingId).NotNull();
    }
}
