using CLup.Application.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public UserData UserData { get; }

    public Address Address { get; }

    public Coords Coords { get; }

    public UpdateUserCommand(UserId userId, UserData userData, Address address, Coords coords)
    {
        UserId = userId;
        UserData = userData;
        Address = address;
        Coords = coords;
    }
}
