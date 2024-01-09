using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo;

public sealed class UpdateUserCommand : IRequest<Result>
{
    public UserId UserId { get; set; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string Zip { get; init; }
    public string Street { get; init; }
    public string City { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
}
