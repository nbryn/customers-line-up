using FluentValidation;

namespace CLup.Application.Queries.User.NotEmployed
{
    public class UsersNotEmployedByBusinessValidator : AbstractValidator<UsersNotEmployedByBusinessQuery>
    {
        public UsersNotEmployedByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}