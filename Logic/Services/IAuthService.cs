using System.Threading.Tasks;

using Logic.Models;

namespace Logic.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> RegisterUser(RegisterDTO user);

        Task<LoginResponseDTO> Authenticate(LoginDTO user); 
    }
}