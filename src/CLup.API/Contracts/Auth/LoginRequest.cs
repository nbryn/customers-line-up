using CLup.Application.Auth.Commands.Login;

namespace CLup.API.Contracts.Auth;

public readonly record struct LoginRequest(string Email, string Password)
{
    public LoginCommand MapToCommand() => new(Email, Password);
}
