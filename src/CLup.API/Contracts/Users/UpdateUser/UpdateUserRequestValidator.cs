namespace CLup.API.Contracts.Users.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
    }
}
