using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.User.General
{
    public class UserInfoQuery : IRequest<Result<UserDto>>
    {
        public string Email { get; set; }

        public UserInfoQuery(string email) => Email = email;
    }
}

