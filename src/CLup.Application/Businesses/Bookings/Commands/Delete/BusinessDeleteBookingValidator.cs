using FluentValidation;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{
    public class BusinessDeleteBookingValidator : AbstractValidator<BusinessDeleteBookingCommand>
    {
        public BusinessDeleteBookingValidator()
        {
            RuleFor(b => b.OwnerEmail).NotNull();
            RuleFor(b => b.BookingId).NotNull();
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}