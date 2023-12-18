using CLup.Domain.Bookings;
using FluentValidation;

namespace CLup.Application.Bookings.Commands.CreateBooking
{
    public class BookingDomainValidator : AbstractValidator<Booking>
    {
        public BookingDomainValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TimeSlotId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}