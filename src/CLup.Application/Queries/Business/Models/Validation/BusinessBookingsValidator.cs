using FluentValidation;

namespace CLup.Application.Queries.Business.Models.Validation
{
    public class BusinessBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public BusinessBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}