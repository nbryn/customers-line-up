using System;

using CLup.Employees;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class EmployeeCreator
    {
        public static Employee Create(DateTime createdAt, int businessId, string UserEmail)
        {
            Employee Employee = new Employee
            {
                CreatedAt = createdAt,
                BusinessId = businessId,
                UserEmail = UserEmail
            };

            return Employee;
        }
    }
}