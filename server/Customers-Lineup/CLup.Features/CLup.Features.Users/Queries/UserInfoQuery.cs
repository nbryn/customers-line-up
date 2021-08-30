using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Users.Queries
{
    public class UserInfoQuery : IRequest<Result<UserDTO>>
    {
        public string Email { get; set; }

        public UserInfoQuery(string email) => Email = email;
    }
}

