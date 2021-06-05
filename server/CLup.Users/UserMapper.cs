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

            CreateMap<NewUserRequest, User>();
        }
    }
}