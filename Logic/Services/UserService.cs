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
    }
}