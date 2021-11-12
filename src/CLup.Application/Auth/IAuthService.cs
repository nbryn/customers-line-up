namespace CLup.Application.Auth
{
    public interface IAuthService
    {
       string GenerateJwtToken(string email);
    }
}