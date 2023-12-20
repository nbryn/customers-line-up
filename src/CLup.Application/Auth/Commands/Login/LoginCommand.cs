namespace CLup.Application.Auth.Commands.Login;

using CLup.Application.Shared;
using MediatR;
using Shared.Result;

public class LoginCommand : IRequest<Result<TokenResponse>>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
