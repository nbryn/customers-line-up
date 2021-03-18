using System.Threading.Tasks;

using CLup.Businesses;
using CLup.Businesses.Interfaces;
using CLup.Util;
using CLup.Employees.DTO;
using CLup.Employees.Interfaces;
using CLup.Users;
using CLup.Users.Interfaces;

namespace CLup.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository, 
            IUserRepository userRepository,
            IBusinessRepository businessRepository)
        {
            _employeeRepository = employeeRepository;
            _businessRepository = businessRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse> VerifyNewEmployee(NewEmployeeRequest request)
        {
            User user = await _userRepository.FindUserByEmail(request.PrivateEmail);

            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            if (user == null || business == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            var response = await _employeeRepository.CreateEmployee(request);

            return response;
        }

    }
}