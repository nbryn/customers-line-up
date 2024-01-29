using FluentValidation;

namespace CLup.API.Contracts.Users.UsersNotEmployedByBusiness;

public class UsersNotEmployedByBusinessRequestValidator : AbstractValidator<UsersNotEmployedByBusinessRequest>
{
    public UsersNotEmployedByBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotNull();
    }
}
