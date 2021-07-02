using System;

using CLup.Domain;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class EmployeeCreator
    {
        public static Employee Create(
            DateTime createdAt, string businessId,
            string userEmail, string companyEmail)
        {
            Employee Employee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = createdAt,
                BusinessId = businessId,
                UserEmail = userEmail,
                CompanyEmail = companyEmail,
                UpdatedAt = DateTime.Now
            };

            return Employee;
        }
    }
}