using CLup.Application.Auth.Commands.Login;

namespace CLup.API.Auth.Contracts;

public readonly record struct LoginRequest(string Email, string Password)
{
    public LoginCommand MapToCommand() => new(Email, Password);
}
