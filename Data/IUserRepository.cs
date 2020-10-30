using System.Threading.Tasks;

using Logic.Models;

namespace Data
{
    public interface IUserRepository
    {
        public Task<int> Create(UserDTO user);
    }
}