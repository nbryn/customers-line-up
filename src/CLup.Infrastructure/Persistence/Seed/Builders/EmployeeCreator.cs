using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Infrastructure.Persistence.Seed.Builders;

public static class EmployeeCreator
{
    public static Employee Create(
        BusinessId businessId,
        UserId userId,
        string companyEmail)
    {
        var employee = new Employee(userId, businessId, companyEmail);
        employee.UpdatedAt = DateTime.UtcNow;

        return employee;
    }
}
