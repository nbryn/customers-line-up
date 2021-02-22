using System.Threading.Tasks;
using System.Collections.Generic;

using Logic.Context;
using Logic.DTO;
using Logic.Employees;

namespace Data
{
    public interface IEmployeeRepository
    {
        Task<HttpCode> CreateEmployee(NewEmployeeRequest request);
        Task<HttpCode> DeleteEmployee(string email, int businessId);
        Task<Employee> FindEmployeeByEmailAndBusiness(string email, int businessId);
        Task<Employee> FindEmployeeByEmail(string email);
        Task<IList<Employee>> FindEmployeesByBusiness(int businessId);
        


    }
}