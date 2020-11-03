using System.Threading.Tasks;

using Logic.Users.Models;

namespace Logic.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> RegisterUser(RegisterDTO user);

        Task<LoginResponseDTO> Authenticate(LoginDTO user); 
    }
}