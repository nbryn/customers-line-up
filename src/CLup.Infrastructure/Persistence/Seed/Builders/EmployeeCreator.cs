using System;
using CLup.Domain.Businesses.Employees;

namespace CLup.Infrastructure.Persistence.Seed.Builders
{
    public static class EmployeeCreator
    {
        public static Employee Create(
            string businessId,
            string userId, 
            string companyEmail)
        {
            var employee = new Employee(userId, businessId, companyEmail);
            employee.UpdatedAt = DateTime.Now;

            return employee;
        }
    }
}