using FluentValidation;

namespace CLup.Features.Users.Queries.Validation
{
    public class UsersNotEmployedByBusinessValidator : AbstractValidator<UsersNotEmployedByBusinessQuery>
    {
        public UsersNotEmployedByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}