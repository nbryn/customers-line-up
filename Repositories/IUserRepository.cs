using System.Threading.Tasks;

using CLup.Models;

namespace CLup.Repositories
{
    public interface IUserRepository
    {
        public Task<int> Create(UserDTO user);
    }
}