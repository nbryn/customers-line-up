using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Employees;

public sealed class Employee : Entity
{
    public EmployeeId Id { get; }

    public UserId UserId { get; private set; }

    public User? User { get; private set; }

    public BusinessId BusinessId { get; private set; }

    public Business? Business { get; private set; }

    public string CompanyEmail { get; private set; }

    public Employee(UserId userId, BusinessId businessId, string companyEmail)
    {
        Guard.Against.Null(userId);
        Guard.Against.Null(businessId);

        UserId = userId;
        BusinessId = businessId;
        CompanyEmail = companyEmail;

        Id = EmployeeId.Create(Guid.NewGuid());
    }

    private Employee()
    {
    }
}
