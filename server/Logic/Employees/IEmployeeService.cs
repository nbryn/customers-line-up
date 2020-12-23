using System.Threading.Tasks;
using System.Collections.Generic;

using Logic.Context;
using Logic.DTO;
using Logic.Employees;

namespace Logic.Employees
{
    public interface IEmployeeService
    {
         Task<Response> VerifyNewEmployee(NewEmployeeDTO request);
    }
}