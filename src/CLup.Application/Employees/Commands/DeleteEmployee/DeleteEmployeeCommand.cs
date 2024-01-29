using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.Employees.Commands.DeleteEmployee;

public sealed class DeleteEmployeeCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessId BusinessId { get; }

    public EmployeeId EmployeeId { get; }

    public DeleteEmployeeCommand(UserId ownerId, BusinessId businessId, EmployeeId employeeId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        EmployeeId = employeeId;
    }
}
