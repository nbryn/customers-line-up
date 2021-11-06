using AutoMapper;
using CLup.Application.Auth;
using CLup.Application.Users.Commands;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()))
                .ForMember(u => u.Email, s => s.MapFrom(m => m.Email))
                .ForMember(u => u.Name, s => s.MapFrom(m => m.UserData.Name))
                .ForMember(u => u.Street, s => s.MapFrom(m => m.Address.Street))
                .ForMember(u => u.City, s => s.MapFrom(m => m.Address.City))
                .ForMember(u => u.Zip, s => s.MapFrom(m => m.Address.Zip))
                .ForMember(u => u.Latitude, s => s.MapFrom(m => m.Coords.Latitude))
                .ForMember(u => u.Longitude, s => s.MapFrom(m => m.Coords.Longitude))
                .ForMember(u => u.Token, s => s.MapFrom<AuthTokenResolver>());

            CreateMap<RegisterCommand, User>()
                .ForMember(u => u.UserData, s => s.MapFrom(s => new UserData(s.Name, s.Email, BC.HashPassword(s.Password))))
                .ForMember(u => u.Address, s => s.MapFrom(s => new Address(s.Street, s.Zip, s.City)))
                .ForMember(u => u.Coords, s => s.MapFrom(s => new Coords(s.Longitude, s.Latitude)));

        }
    }
}