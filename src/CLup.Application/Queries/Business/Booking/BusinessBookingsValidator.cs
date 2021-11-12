using FluentValidation;

namespace CLup.Application.Queries.Business.Booking
{
    public class BusinessBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public BusinessBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}