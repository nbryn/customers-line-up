using CLup.Application.Employees.Commands.DeleteEmployee;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using EId = CLup.Domain.Employees.ValueObjects.EmployeeId;

namespace CLup.API.Contracts.Employees.DeleteEmployee;

public record struct DeleteEmployeeRequest()
{
    [FromQuery]
    public Guid BusinessId { get; set; }

    [FromRoute]
    public Guid EmployeeId { get; set; }

    public DeleteEmployeeCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), EId.Create(EmployeeId));
}
