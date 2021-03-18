using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Context;
using CLup.Employees.DTO;
using CLup.Util;

namespace CLup.Employees.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<ServiceResponse> CreateEmployee(NewEmployeeRequest request);
        Task<ServiceResponse> DeleteEmployee(string email, int businessId);
        Task<Employee> FindEmployeeByEmailAndBusiness(string email, int businessId);
        Task<ServiceResponse<EmployeeDTO>> FindEmployeeByEmail(string email);
        Task<ServiceResponse<IList<EmployeeDTO>>> FindEmployeesByBusiness(int businessId);
        


    }
}