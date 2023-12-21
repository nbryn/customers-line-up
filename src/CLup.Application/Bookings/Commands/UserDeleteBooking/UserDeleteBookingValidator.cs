using FluentValidation;

namespace CLup.Application.Bookings.Commands.UserDeleteBooking;

public sealed class UserDeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
{
    public UserDeleteBookingValidator()
    {
        RuleFor(b => b.BookingId).NotNull();
    }
}
