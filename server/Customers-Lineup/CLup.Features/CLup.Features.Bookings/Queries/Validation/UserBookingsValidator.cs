using FluentValidation;

namespace CLup.Features.Bookings.Queries.Validation
{
    public class UserBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public UserBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}