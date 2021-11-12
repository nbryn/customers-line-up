using FluentValidation;

namespace CLup.Application.Queries.User.Models.Validation
{
    public class UsersNotEmployedByBusinessValidator : AbstractValidator<UsersNotEmployedByBusinessQuery>
    {
        public UsersNotEmployedByBusinessValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}