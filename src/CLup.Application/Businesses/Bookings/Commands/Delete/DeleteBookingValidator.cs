using FluentValidation;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{
    public class DeleteBookingValidator : AbstractValidator<BusinessDeleteBookingCommand>
    {
        public DeleteBookingValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
            RuleFor(b => b.BookingId).NotNull();
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}