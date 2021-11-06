using FluentValidation;

namespace CLup.Application.Bookings.Commands.Validation
{
    public class UserDeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
    {
        public UserDeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}