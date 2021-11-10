using FluentValidation;

namespace CLup.Application.Commands.User.Models.Validation
{
    public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(b => b.BookingId).NotNull();
        }
    }
}