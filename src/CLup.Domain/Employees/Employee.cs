using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Employees;

public sealed class Employee : Entity<EmployeeId>
{
    public UserId UserId { get; private set; }

    public BusinessId BusinessId { get; private set; }

    public string CompanyEmail { get; private set; }

    public Employee(UserId userId, BusinessId businessId, string companyEmail)
    {
        this.UserId = userId;
        this.BusinessId = businessId;
        this.CompanyEmail = companyEmail;
    }
}
