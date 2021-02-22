using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO.User;
using Logic.Users;

namespace Data
{
    public interface IUserRepository
    {
        Task<User> FindUserByEmail(string email);
        Task<int> CreateUser(NewUserRequest user);
        Task<User> FindUserById(int userId);
        Task<IList<User>> GetAll();

    }
}