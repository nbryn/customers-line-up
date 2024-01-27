namespace CLup.Application.Auth.Commands.Login;

using FluentValidation;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(command => command.Password).NotEmpty();
    }
}
