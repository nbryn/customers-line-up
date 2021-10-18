using System;

using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
{
    public static class EmployeeCreator
    {
        public static Employee Create(
            DateTime createdAt, string businessId,
            string userId, string companyEmail)
        {
            Employee Employee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = createdAt,
                BusinessId = businessId,
                UserId = userId,
                CompanyEmail = companyEmail,
                UpdatedAt = DateTime.Now
            };

            return Employee;
        }
    }
}