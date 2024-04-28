namespace CLup.API.Users.Contracts.UsersNotEmployedByBusiness;

public class UsersNotEmployedByBusinessRequestValidator : AbstractValidator<UsersNotEmployedByBusinessRequest>
{
    public UsersNotEmployedByBusinessRequestValidator()
    {
        RuleFor(request => request.BusinessId).NotNull();
    }
}
