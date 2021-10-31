using AutoMapper;

using CLup.Domain.Users;
using CLup.Features.Auth;

namespace CLup.Features.Users
{
    public class AuthTokenResolver : IValueResolver<User, UserDTO, string>
    {
        private readonly IAuthService _authService;

        public AuthTokenResolver(IAuthService authService) => _authService = authService;

        public string Resolve(User user, UserDTO dto, string token, ResolutionContext context)
            => _authService.GenerateJWTToken(user.Email);
    }
}