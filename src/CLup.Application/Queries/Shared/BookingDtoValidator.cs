using FluentValidation;

namespace CLup.Application.Queries.Shared
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.TimeSlotId).NotEmpty();
            RuleFor(x => x.Street).NotEmpty().Length(0, 75);
            RuleFor(x => x.Business).NotEmpty();
            RuleFor(x => x.UserEmail).EmailAddress();
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}