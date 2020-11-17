using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;

using Data;
using Logic.Auth;
using Logic.Users;
using Logic.Exceptions;
using Logic.DTO.User;

namespace Logic.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository repository, IAuthService authService)
        {
            _authService = authService;
            _repository = repository;
        }

        public async Task<LoginResponseDTO> RegisterUser(RegisterDTO user)
        {
            User userExists = await _repository.FindUserByEmail(user.Email);

            if (userExists != null)
            {
                // TODO: Email already exists in system
            }

            string token = _authService.GenerateJWTToken(user);
            user.Password = BC.HashPassword(user.Password);

            int userId = await _repository.CreateUser(user);

            LoginResponseDTO response = new LoginResponseDTO
            {
                Id = userId,
                Email = user.Email,
                Token = token,
            };

            return response;
        }
        public async Task<LoginResponseDTO> AuthenticateUser(LoginDTO loginRequest)
        {
            User user = await _repository.FindUserByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                // TODO: User does not exist / wrong password
            }

            string token = _authService.GenerateJWTToken(loginRequest);

            LoginResponseDTO response = new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
            };

            return response;
        }
    }
}