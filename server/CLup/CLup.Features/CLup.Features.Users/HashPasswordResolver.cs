using AutoMapper;
using BC = BCrypt.Net.BCrypt;

using CLup.Domain;
using CLup.Features.Auth;

namespace CLup.Features.Users
{
    public class HashPasswordResolver : IValueResolver<RegisterUser.Command, User, string>
    {
        private readonly IAuthService _authService;

        public HashPasswordResolver(IAuthService authService) => _authService = authService;
        
        public string Resolve(RegisterUser.Command command, User user, string token, ResolutionContext context)
            => BC.HashPassword(command.Password);    
    }

}