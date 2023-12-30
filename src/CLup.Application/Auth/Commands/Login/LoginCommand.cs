using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginCommand : IRequest<Result<TokenResponse>>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
