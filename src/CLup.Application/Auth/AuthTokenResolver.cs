using AutoMapper;
using CLup.Application.Shared.Models;
using CLup.Domain.User;
using CLup.Domain.Users;

namespace CLup.Application.Auth
{
    public class AuthTokenResolver : IValueResolver<User, UserDto, string>
    {
        private readonly IAuthService _authService;

        public AuthTokenResolver(IAuthService authService) => _authService = authService;

        public string Resolve(User user, UserDto dto, string token, ResolutionContext context)
            => _authService.GenerateJwtToken(user.Email);
    }
}