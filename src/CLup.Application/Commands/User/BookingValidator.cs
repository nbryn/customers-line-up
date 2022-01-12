using FluentValidation;

namespace CLup.Domain.Booking
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TimeSlotId).NotEmpty();
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}