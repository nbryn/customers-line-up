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
        Task<IList<Employee>> FindEmployeesByBusiness(int businessId);
        // Task<Employee> FindEmployeeByIdAndBusiness(int businessId, string email);


    }
}