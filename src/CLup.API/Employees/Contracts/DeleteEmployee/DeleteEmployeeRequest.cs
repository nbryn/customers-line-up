using CLup.Application.Employees.Commands.DeleteEmployee;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using EId = CLup.Domain.Employees.ValueObjects.EmployeeId;

namespace CLup.API.Employees.Contracts.DeleteEmployee;

public readonly record struct DeleteEmployeeRequest(Guid EmployeeId, Guid BusinessId)
{
    public DeleteEmployeeCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), EId.Create(EmployeeId));
}
