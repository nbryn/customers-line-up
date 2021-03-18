using System;

using CLup.Employees;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class EmployeeCreator
    {
        public static Employee Create(int id, DateTime createdAt, int businessId, string UserEmail)
        {
            Employee Employee = new Employee
            {
                Id = id,
                CreatedAt = createdAt,
                BusinessId = businessId,
                UserEmail = UserEmail
            };

            return Employee;
        }
    }
}