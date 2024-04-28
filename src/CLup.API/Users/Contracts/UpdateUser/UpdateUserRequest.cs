using CLup.Application.Users.Commands.UpdateUser;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.API.Users.Contracts.UpdateUser;

public readonly record struct UpdateUserRequest(string Email, string Name, Address Address)
{
    public UpdateUserCommand MapToCommand(UserId userId) => new(userId, Address, Name, Email);
}
