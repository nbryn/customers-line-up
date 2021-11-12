using FluentValidation;

namespace CLup.Application.Queries.User.Models.Validation
{
    public class UserInfoValidator : AbstractValidator<UserInfoQuery>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}