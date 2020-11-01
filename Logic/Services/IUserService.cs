using System.Threading.Tasks;

using Logic.Models;

namespace Logic.Services
{
    public interface IUserService
    {
        Task<int> RegisterUser(RegisterDTO user);

        Task<UserDTO> Authenticate(LoginDTO user);
         
    }
}