namespace CLup.Features.Auth
{
    public interface IAuthService
    {
       string GenerateJWTToken(string email);
    }
}