using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.Models;

namespace Data
{
    public interface IUserRepository
    {
        Task<int> Create(UserDTO user);
        Task<UserDTO> Read(int userId);
        IEnumerable<UserDTO> Read();

    }
}