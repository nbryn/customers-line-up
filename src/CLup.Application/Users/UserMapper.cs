using System.Linq;
using AutoMapper;
using CLup.Domain.Users;

namespace CLup.Application.Users;

public sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.ToString()))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.UserData.Name))
            .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Zip, opts => opts.MapFrom(src => src.Address.Zip))
            .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Coords.Latitude))
            .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Coords.Longitude))
            .ForMember(dest => dest.Messages,
                opts => opts.MapFrom(src => src.ReceivedMessages.Concat(src.SentMessages)));
    }
}
