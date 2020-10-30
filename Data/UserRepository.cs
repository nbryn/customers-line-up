using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Logic.Models;

namespace Data
{
    public class UserRepository : IUserRepository
    {
       public Task<int> Create(UserDTO user)
        {
            return null;
        }
    }
}

