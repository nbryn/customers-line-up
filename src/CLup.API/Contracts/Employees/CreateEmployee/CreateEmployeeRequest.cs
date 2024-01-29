using CLup.Application.Employees.Commands.CreateEmployee;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using UId = CLup.Domain.Users.ValueObjects.UserId;

namespace CLup.API.Contracts.Employees.CreateEmployee;

public readonly record struct CreateEmployeeRequest(
    Guid BusinessId,
    Guid UserId,
    string CompanyEmail)
{
    public CreateEmployeeCommand MapToCommand(UserId ownerId) =>
        new(ownerId, BId.Create(BusinessId), UId.Create(UserId), CompanyEmail);
}
