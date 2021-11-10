using CLup.Application.Queries.User;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Auth
{
    public class LoginCommand : IRequest<Result<UserDto>>
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }
}

