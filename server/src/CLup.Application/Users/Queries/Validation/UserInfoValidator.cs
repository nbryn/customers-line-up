using FluentValidation;

namespace CLup.Application.Users.Queries.Validation
{
    public class UserInfoValidator : AbstractValidator<UserInfoQuery>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}