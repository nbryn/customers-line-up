using System.Threading.Tasks;

using Data;
using Logic.Users;
using Logic.Exceptions;
using Logic.Users.Models;


namespace Logic.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
    }
}