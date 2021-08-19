using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Users.Commands
{
    public class LoginCommand : IRequest<Result<UserDTO>>
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }
}

