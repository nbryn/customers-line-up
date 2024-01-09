using System;
using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; init; }

    public Guid UserId { get; init; }

    public DeleteEmployeeCommand(UserId ownerId, Guid businessId, Guid userId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        UserId = userId;
    }
}
