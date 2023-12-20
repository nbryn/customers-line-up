using CLup.Domain.Users;

namespace CLup.Application.Auth;

public interface IAuthService
{
    string GenerateJwtToken(User user);
}
