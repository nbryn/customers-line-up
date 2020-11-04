using System.Threading.Tasks;

using Logic.Users.Models;

namespace Logic.Auth
{
    public interface IAuthService
    {
       string GenerateJWTToken(LoginDTO user);
    }
}