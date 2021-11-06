using FluentValidation;

namespace CLup.Application.Bookings.Queries.Validation
{
    public class BusinessBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public BusinessBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}