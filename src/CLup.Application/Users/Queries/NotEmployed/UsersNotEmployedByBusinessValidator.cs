using FluentValidation;

namespace CLup.Application.Users.Queries.NotEmployed
{
    public class UsersNotEmployedByBusinessValidator : AbstractValidator<UsersNotEmployedByBusinessQuery>
    {
        public UsersNotEmployedByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}