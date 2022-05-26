using FluentValidation;

namespace CLup.Application.Users.Bookings.Commands.Delete
{
    public class UserDeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
    {
        public UserDeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}