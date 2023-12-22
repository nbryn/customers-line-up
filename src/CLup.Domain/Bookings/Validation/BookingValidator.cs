using FluentValidation;

namespace CLup.Domain.Bookings.Validation;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(booking => booking.User).NotEmpty();
        RuleFor(booking => booking.TimeSlot).NotEmpty();
        RuleFor(booking => booking.Business).NotEmpty();
    }
}
