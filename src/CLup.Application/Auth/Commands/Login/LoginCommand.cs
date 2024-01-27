using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginCommand : IRequest<Result<TokenResponse>>
{
    public UserId Id { get; set; }

    public string Password { get; init; }
}
