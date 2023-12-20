using AutoMapper;
using CLup.Domain.Users;

namespace CLup.Application.Auth;

public class AuthTokenResolver : IValueResolver<User, TokenResponse, string>
{
    private readonly IAuthService _authService;

    public AuthTokenResolver(IAuthService authService) => _authService = authService;

    public string Resolve(User user, TokenResponse response, string token, ResolutionContext context)
        => _authService.GenerateJwtToken(user);
}
