using FluentValidation;

namespace CLup.Application.Bookings.Queries.Validation
{
    public class UserBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public UserBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}