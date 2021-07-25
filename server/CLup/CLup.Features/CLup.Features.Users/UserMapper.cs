using System;

using AutoMapper;
using BC = BCrypt.Net.BCrypt;

using CLup.Domain;
using CLup.Features.Auth;

namespace CLup.Features.Users
{
    public class UserMapper : Profile
    {
        private readonly IAuthService _authService; 
        public UserMapper(IAuthService authService)
        {
            _authService = authService;

            CreateMap<User, UserDTO>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()))
                .ForMember(u => u.Token, s => s.MapFrom(m => _authService.GenerateJWTToken(m.Email)));

            CreateMap<RegisterUser.Command, User>()
                .ForMember(u => u.Id, s => s.MapFrom(m => Guid.NewGuid().ToString()))
                .ForMember(u => u.Password, s => s.MapFrom(m => BC.HashPassword(m.Password)))
                .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
                .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));
        }
    }
}