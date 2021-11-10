using CLup.Application.Queries.Business.Models;
using FluentValidation;

namespace CLup.Application.Queries.User.Models.Validation
{
    public class UserBookingsValidator : AbstractValidator<BusinessBookingsQuery>
    {
        public UserBookingsValidator()
        {
            RuleFor(b => b.BusinessId).NotNull();
        }
    }
}