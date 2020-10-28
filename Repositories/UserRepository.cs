using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CLup.Models;

namespace CLup.Repositories
{
    public class UserRepository : IUserRepository
    {
       public Task<int> Create(UserDTO user)
        {
            return null;
        }
    }
}

