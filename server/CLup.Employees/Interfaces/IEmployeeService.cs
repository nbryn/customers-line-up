using System.Threading.Tasks;

using CLup.Employees.DTO;
using CLup.Util;

namespace CLup.Employees.Interfaces
{
    public interface IEmployeeService
    {
         Task<ServiceResponse> VerifyNewEmployee(NewEmployeeRequest request);
    }
}