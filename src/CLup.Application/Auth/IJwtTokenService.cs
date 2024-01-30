using CLup.Domain.Users;

namespace CLup.Application.Auth;

public interface IJwtTokenService
{
    string GenerateJwtToken(User user);
}
