using FluentValidation;

namespace CLup.Features.Bookings.Commands.Validation
{
    public class UserDeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
    {
        public UserDeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}