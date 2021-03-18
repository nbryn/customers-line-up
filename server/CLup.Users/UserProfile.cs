using AutoMapper;

using CLup.Users.DTO;

namespace CLup.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Role, s => s.MapFrom(m => m.Role.ToString()));
        }
    }
}