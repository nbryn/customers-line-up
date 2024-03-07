using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessId BusinessId { get; }

    public UserId UserId { get; }

    public string? CompanyEmail { get; }

    public CreateEmployeeCommand(UserId ownerId, BusinessId businessId, UserId userId, string? companyEmail)
    {
        OwnerId = ownerId;
        Domain.Businesses.ValueObjects.BusinessId = businessId;
        Domain.Users.ValueObjects.UserId = userId;
        CompanyEmail = companyEmail;
    }

    public Employee MapToEmployee(User user) => new(Domain.Users.ValueObjects.UserId, Domain.Businesses.ValueObjects.BusinessId, CompanyEmail ?? user.UserData.Email);
}
