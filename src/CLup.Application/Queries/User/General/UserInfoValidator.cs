using FluentValidation;

namespace CLup.Application.Queries.User.General
{
    public class UserInfoValidator : AbstractValidator<UserInfoQuery>
    {
        public UserInfoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}