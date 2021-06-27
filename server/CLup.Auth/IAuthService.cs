using CLup.Users.DTO;

namespace CLup.Auth
{
    public interface IAuthService
    {
       string GenerateJWTToken(string email);
    }
}