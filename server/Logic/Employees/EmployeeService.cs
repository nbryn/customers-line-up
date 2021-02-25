using System.Threading.Tasks;

using Data;
using Logic.Businesses;
using Logic.DTO;
using Logic.Users;

namespace Logic.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository,
                               IBusinessRepository businessRepository)
        {
            _employeeRepository = employeeRepository;
            _businessRepository = businessRepository;
            _userRepository = userRepository;
        }

        public async Task<HttpCode> VerifyNewEmployee(NewEmployeeRequest request)
        {
            User user = await _userRepository.FindUserByEmail(request.PrivateEmail);

            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            if (user == null || business == null)
            {
                return HttpCode.NotFound;
            }

            HttpCode response = await _employeeRepository.CreateEmployee(request);

            return response;
        }

    }
}