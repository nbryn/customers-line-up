using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Auth;
using CLup.Businesses.Interfaces;
using CLup.Util;
using CLup.Employees.Interfaces;
using CLup.Users.DTO;
using CLup.Users.Interfaces;
namespace CLup.Users
{
    public class UserService : IUserService
    {
        private readonly IBusinessOwnerRepository _ownerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public UserService(
            IBusinessOwnerRepository ownerRepository,
            IEmployeeRepository employeeRepository,
            IUserRepository userRepository,
            IAuthService authService,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDTO>> RegisterUser(NewUserRequest user)
        {
            User userExists = await _userRepository.FindUserByEmail(user.Email);

            if (userExists != null)
            {
                return new ServiceResponse<UserDTO>(HttpCode.Conflict, $"An existing user with the email '{user.Email}' was found.");
            }

            string token = _authService.GenerateJWTToken(user);
            user.Password = BC.HashPassword(user.Password);

            var userId = await _userRepository.CreateUser(user);

            var response = _mapper.Map<UserDTO>(user);

            return new ServiceResponse<UserDTO>(HttpCode.Ok, response);
        }
        public async Task<ServiceResponse<UserDTO>> AuthenticateUser(LoginRequest loginRequest)
        {
            User user = await _userRepository.FindUserByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                return new ServiceResponse<UserDTO>(HttpCode.Unauthorized);
            }

            string token = _authService.GenerateJWTToken(loginRequest);

            await DetermineRole(user);

            var response = _mapper.Map<UserDTO>(user);
            response.Token = token;

            return new ServiceResponse<UserDTO>(HttpCode.Ok, response);
        }

        public async Task<ServiceResponse<IList<UserDTO>>> FilterUsersByBusiness(string businessId)
        {
            var notAlreadyEmployedByBusiness = new List<UserDTO>();

            var allUsers = await _userRepository.GetAll();

            foreach (UserDTO user in allUsers._response)
            {
                if (await _employeeRepository.FindEmployeeByEmailAndBusiness(user.Email, businessId) == null)
                {
                    notAlreadyEmployedByBusiness.Add(user);
                }
            }

            return new ServiceResponse<IList<UserDTO>>(HttpCode.Ok, notAlreadyEmployedByBusiness);
        }

        public async Task DetermineRole(User user)
        {
            if (await _ownerRepository.FindOwnerByEmail(user.Email) != null)
            {
                user.Role = Role.Owner;

                return;
            }
            var isEmployee = await _employeeRepository.FindEmployeeByEmail(user.Email);

            if (isEmployee._statusCode != HttpCode.NotFound)
            {
                user.Role = Role.Employee;

                return;
            }

            user.Role = Role.User;
        }
    }
}