using CLup.Application.Queries.Business.Booking;
using FluentValidation;

namespace CLup.Application.Queries.User.Booking
{
    public class UserBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public UserBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}