using AutoMapper;
using BC = BCrypt.Net.BCrypt;

using CLup.Domain;
using CLup.Features.Auth;
using CLup.Features.Users.Commands;

namespace CLup.Features.Users
{
    public class HashPasswordResolver : IValueResolver<RegisterCommand, User, string>
    {
        private readonly IAuthService _authService;

        public HashPasswordResolver(IAuthService authService) => _authService = authService;
        
        public string Resolve(RegisterCommand command, User user, string token, ResolutionContext context)
            => BC.HashPassword(command.Password);    
    }

}