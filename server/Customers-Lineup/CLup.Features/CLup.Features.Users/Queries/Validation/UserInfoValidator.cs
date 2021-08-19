using FluentValidation;

namespace CLup.Features.Users.Queries.Validation
{
    public class UserInfoValidator : AbstractValidator<UserInfoQuery>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}