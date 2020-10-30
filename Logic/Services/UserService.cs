using System.Threading.Tasks;

using Data;
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
        public Task<int> RegisterUser(UserDTO user)
        {
            return _repository.Create(user);
        }

    }
}