using System;

using AutoMapper;

using CLup.Domain;
using CLup.Features.Users.Commands;

namespace CLup.Features.Users
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()))
                .ForMember(u => u.Token, s => s.MapFrom<AuthTokenResolver>());

            CreateMap<RegisterCommand, User>()
                .ForMember(u => u.Id, s => s.MapFrom(m => Guid.NewGuid().ToString()))
                .ForMember(u => u.Password, s => s.MapFrom<HashPasswordResolver>())
                .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
                .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));

            CreateMap<UpdateUserInfoCommand, User>().ConvertUsing<UpdateUserInfoConverter>();
        }
    }
}