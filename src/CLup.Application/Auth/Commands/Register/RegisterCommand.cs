using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Auth.Commands.Register;

public sealed class RegisterCommand : IRequest<Result<TokenResponse>>
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string Name { get; init; }

    public string Zip { get; init; }

    public string Street { get; init; }

    public string City { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }
}
