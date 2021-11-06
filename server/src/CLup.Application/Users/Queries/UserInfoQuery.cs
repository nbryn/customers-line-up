using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Users.Queries
{
    public class UserInfoQuery : IRequest<Result<UserDto>>
    {
        public string Email { get; set; }

        public UserInfoQuery(string email) => Email = email;
    }
}

