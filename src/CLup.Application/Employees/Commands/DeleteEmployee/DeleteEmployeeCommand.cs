using System;
using CLup.Application.Shared.Result;
using MediatR;

namespace CLup.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid UserId { get; set; }

    public DeleteEmployeeCommand(Guid ownerId, Guid businessId, Guid userId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        UserId = userId;
    }
}
