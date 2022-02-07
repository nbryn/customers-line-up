using CLup.Domain.Bookings;
using FluentValidation;

namespace CLup.Application.Users.Bookings.Commands.Create
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