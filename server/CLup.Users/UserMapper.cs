using System;

using AutoMapper;

using CLup.Users.DTO;

namespace CLup.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()));

            CreateMap<RegisterUser.Command, User>()
                .ForMember(u => u.Id, s => s.MapFrom(m => Guid.NewGuid().ToString()))
                .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
                .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));
        }
    }
}