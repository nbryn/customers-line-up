using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;


using Data;
using Logic.Entities;
using Logic.Exceptions;
using Logic.Models;


namespace Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> RegisterUser(RegisterDTO user)
        {
            user.Password = BC.HashPassword(user.Password);
            return await _repository.Register(user);
        }

        public async Task<UserDTO> Authenticate(LoginDTO loginRequest)
        {

            UserDTO user = await _repository.FindByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                throw new AuthenticationFailedException("Invalid email/password combination");
            }

            return user;
        }

    }
}