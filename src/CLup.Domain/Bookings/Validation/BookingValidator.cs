namespace CLup.Domain.Bookings.Validation;

public class BookingValidator : AbstractValidator<Booking>
{
    public BookingValidator()
    {
        RuleFor(booking => booking.UserId).NotEmpty();
        RuleFor(booking => booking.TimeSlotId).NotEmpty();
        RuleFor(booking => booking.BusinessId).NotEmpty();
    }
}
