using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.DTO.User;

namespace Data
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUserByEmail(string email);
        Task<int> CreateUser(RegisterDTO user);
        Task<UserDTO> GetUserById(int userId);
        IQueryable<UserDTO> Read();

    }
}