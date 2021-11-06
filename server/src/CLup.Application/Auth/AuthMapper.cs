using AutoMapper;
using CLup.Application.Users.Commands;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(u => u.UserData, s => s.MapFrom(s => new UserData(s.Name, s.Email, BC.HashPassword(s.Password))))
                .ForMember(u => u.Address, s => s.MapFrom(s => new Address(s.Street, s.Zip, s.City)))
                .ForMember(u => u.Coords, s => s.MapFrom(s => new Coords(s.Longitude, s.Latitude)));

        }
    }
}