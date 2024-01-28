using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginCommand : IRequest<Result<string>>
{
    public string Email { get; }

    public string Password { get; }

    public LoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
