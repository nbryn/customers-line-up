using CLup.Application.Shared;
using CLup.Application.Users;
using MediatR;

namespace CLup.Application.Auth
{
    public class LoginCommand : IRequest<Result<UserDto>>
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }
}

