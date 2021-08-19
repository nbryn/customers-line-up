using FluentValidation;

namespace CLup.Features.Bookings.Commands.Validation
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