using System.Threading.Tasks;
using System.Collections.Generic;

using Logic.Context;
using Logic.DTO;
using Logic.Employees;

namespace Data
{
    public interface IEmployeeRepository
    {
        Task<Response> CreateEmployee(NewEmployeeDTO request);
        Task<Response> DeleteEmployee(string email, int businessId);
        Task<Employee> FindEmployeeByEmailAndBusiness(string email, int businessId);
        Task<IList<Employee>> FindEmployeesByBusiness(int businessId);
        


    }
}