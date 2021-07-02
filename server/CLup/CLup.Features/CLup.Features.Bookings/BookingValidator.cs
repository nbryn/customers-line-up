using FluentValidation;

namespace CLup.Features.Bookings
{
    public class BookingValidator : AbstractValidator<BookingDTO>
    {
        public BookingValidator()
        {
            RuleFor(x => x.TimeSlotId).NotEmpty();
            RuleFor(x => x.Address).NotEmpty().Length(0, 75);
            RuleFor(x => x.Business).NotEmpty();
            RuleFor(x => x.UserEmail).EmailAddress();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}