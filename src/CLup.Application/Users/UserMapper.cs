using System.Linq;
using AutoMapper;
using CLup.Domain.Users;

namespace CLup.Application.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()))
                .ForMember(u => u.Name, s => s.MapFrom(m => m.UserData.Name))
                .ForMember(u => u.Street, s => s.MapFrom(m => m.Address.Street))
                .ForMember(u => u.City, s => s.MapFrom(m => m.Address.City))
                .ForMember(u => u.Zip, s => s.MapFrom(m => m.Address.Zip))
                .ForMember(u => u.Latitude, s => s.MapFrom(m => m.Coords.Latitude))
                .ForMember(u => u.Longitude, s => s.MapFrom(m => m.Coords.Longitude))
                .ForMember(dest => dest.Messages, src => src.MapFrom(user => user.ReceivedMessages.Concat(user.SentMessages)));
        }
    }
}