using System.Threading.Tasks;

using Logic.DTO.User;

namespace Logic.Auth
{
    public interface IAuthService
    {
       string GenerateJWTToken(LoginDTO user);
    }
}