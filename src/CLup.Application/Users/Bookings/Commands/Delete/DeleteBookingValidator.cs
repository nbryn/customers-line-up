using FluentValidation;

namespace CLup.Application.Users.Bookings.Commands.Delete
{
    public class DeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}