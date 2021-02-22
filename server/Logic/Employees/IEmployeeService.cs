using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.Employees
{
    public interface IEmployeeService
    {
         Task<HttpCode> VerifyNewEmployee(NewEmployeeRequest request);
    }
}