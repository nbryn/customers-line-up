using System.Threading.Tasks;

using Logic.DTO.User;

namespace Logic.Users
{
    public interface IUserService
    {
        Task<LoginResponseDTO> RegisterUser(RegisterDTO user);

        Task<LoginResponseDTO> AuthenticateUser(LoginDTO user);

    }
}