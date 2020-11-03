using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.Users.Models;

namespace Data
{
    public interface IUserRepository
    {
        Task<UserDTO>FindByEmail(string email);
        Task<int> Register(RegisterDTO user);
        Task<UserDTO> Read(int userId);
        IQueryable<UserDTO> Read();

    }
}