using System.Threading.Tasks;
using System.Collections.Generic;

using Data;
using Logic.Businesses;
using Logic.Context;
using Logic.DTO;
using Logic.Employees;
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

        public async Task<Response> VerifyNewEmployee(NewEmployeeDTO request)
        {
            User user = await _userRepository.FindUserByEmail(request.PrivateEmail);

            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            if (user == null || business == null)
            {
                return Response.NotFound;
            }

            Response response = await _employeeRepository.CreateEmployee(request);

            return response;
        }

    }
}