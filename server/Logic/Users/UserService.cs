using BC = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Threading.Tasks;

using Data;
using Logic.Auth;
using Logic.DTO.User;

namespace Logic.Users
{
    public class UserService : IUserService
    {
        private readonly IBusinessOwnerRepository _ownerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IBusinessOwnerRepository ownerRepository, IEmployeeRepository employeeRepository,
            IUserRepository userRepository, IAuthService authService)
        {
            _employeeRepository = employeeRepository;
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
            _authService = authService;

        }

        public async Task<LoginResponse> RegisterUser(RegisterUserDTO user)
        {
            User userExists = await _userRepository.FindUserByEmail(user.Email);

            if (userExists != null)
            {
                return new LoginResponse
                {
                    isError = true,
                };
            }

            string token = _authService.GenerateJWTToken(user);
            user.Password = BC.HashPassword(user.Password);

            int userId = await _userRepository.CreateUser(user);

            LoginResponse response = new LoginResponse
            {
                Id = userId,
                Email = user.Email,
                Token = token,
            };

            return response;
        }
        public async Task<LoginResponse> AuthenticateUser(LoginDTO loginRequest)
        {
            User user = await _userRepository.FindUserByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                return new LoginResponse
                {
                    isError = true,
                };
            }

            string token = _authService.GenerateJWTToken(loginRequest);

            Role role = await DetermineRole(user);

            LoginResponse response = new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = token,
                Role = role.ToString(),
            };

            return response;
        }

        public async Task<IList<User>> FilterUsersByBusiness(int businessId)
        {
            IList<User> notAlreadyEmployedByBusiness = new List<User>();

            IList<User> allUsers = await _userRepository.GetAll();

            foreach (User user in allUsers)
            {
                if (await _employeeRepository.FindEmployeeByEmailAndBusiness(user.Email, businessId) == null)
                {
                    notAlreadyEmployedByBusiness.Add(user);
                }
            }

            return notAlreadyEmployedByBusiness;
        }

        public async Task<Role> DetermineRole(User user)
        {
            if (await _ownerRepository.FindOwnerByEmail(user.Email) != null)
            {
                return Role.Owner;
            }
            if (await _employeeRepository.FindEmployeeByEmail(user.Email) != null)
            {
                return Role.Employee;
            }

            return Role.User;
        }
    }
}