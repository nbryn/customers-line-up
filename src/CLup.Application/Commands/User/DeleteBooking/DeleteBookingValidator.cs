using CLup.Application.Commands.User.CreateBooking;
using FluentValidation;

namespace CLup.Application.Commands.User.DeleteBooking
{
    public class DeleteBookingValidator : AbstractValidator<UserDeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}