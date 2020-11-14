using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.DTO.User;
using Logic.Users;

namespace Data
{
    public interface IUserRepository
    {
        Task<User> FindUserByEmail(string email);
        Task<int> CreateUser(RegisterDTO user);
        Task<User> FindUserById(int userId);
        IQueryable<UserDTO> Read();

    }
}