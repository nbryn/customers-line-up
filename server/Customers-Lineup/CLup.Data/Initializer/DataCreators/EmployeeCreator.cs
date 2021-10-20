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
            return new Employee(userId, businessId, companyEmail);
        }
    }
}