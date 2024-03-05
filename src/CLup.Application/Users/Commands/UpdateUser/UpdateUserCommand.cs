using CLup.Application.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Result>
{
    public UserId UserId { get; }

    public Address Address { get; }

    public Coords Coords { get; }

    public string Name { get; }

    public string Email { get; }

    public UpdateUserCommand(UserId userId, Address address, Coords coords, string name, string email)
    {
        UserId = userId;
        Address = address;
        Coords = coords;
        Email = email;
        Name = name;
    }
}
