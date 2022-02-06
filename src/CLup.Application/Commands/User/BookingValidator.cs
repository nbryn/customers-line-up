using FluentValidation;

namespace CLup.Domain.Booking
{
    public class BookingValidator : AbstractValidator<Bookings.Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TimeSlotId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}