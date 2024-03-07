namespace CLup.API.Contracts.Auth.Validation;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Password).NotEmpty();
    }
}
