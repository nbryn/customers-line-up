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
        private readonly IBusinessOwnerRepository _ownerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IBusinessOwnerRepository ownerRepository,
            IUserRepository userRepository, IAuthService authService)
        {
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
            _authService = authService;

        }

        public async Task<LoginResponseDTO> RegisterUser(RegisterDTO user)
        {
            User userExists = await _userRepository.FindUserByEmail(user.Email);

            if (userExists != null)
            {
                // TODO: Email already exists in system
            }

            string token = _authService.GenerateJWTToken(user);
            user.Password = BC.HashPassword(user.Password);

            int userId = await _userRepository.CreateUser(user);

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
            User user = await _userRepository.FindUserByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                // TODO: User does not exist / wrong password
            }

            string token = _authService.GenerateJWTToken(loginRequest);

            bool isOwner = _ownerRepository.FindOwnerByEmail(loginRequest.Email) == null ? false : true;

            LoginResponseDTO response = new LoginResponseDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Token = token,
                isOwner = isOwner
            };

            return response;
        }
    }
}