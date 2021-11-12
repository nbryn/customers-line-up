using FluentValidation;

namespace CLup.Application.Commands.Business.DeleteBooking
{
    public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
            RuleFor(b => b.BookingId).NotNull();
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}