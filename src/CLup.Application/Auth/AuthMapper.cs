using AutoMapper;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;
using BC = BCrypt.Net.BCrypt;
using CLup.Application.Auth.Commands.Register;

namespace CLup.Application.Auth;

public sealed class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<RegisterCommand, User>()
            .ForMember(dest => dest.UserData, opts => opts.MapFrom(src => new UserData(src.Name, src.Email, BC.HashPassword(src.Password))))
            .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new Address(src.Street, src.Zip, src.City)))
            .ForMember(dest => dest.Coords, opts => opts.MapFrom(src => new Coords(src.Longitude, src.Latitude)))
            .ForMember(dest => dest.Role, opts => opts.MapFrom(src => Role.User));

        CreateMap<User, TokenResponse>()
            .ForMember(dest => dest.Token, opts => opts.MapFrom<AuthTokenResolver>());
    }
}
