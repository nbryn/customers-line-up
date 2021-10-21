using System;

using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
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