using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Auth
{
    public class LoginCommand : IRequest<Result<TokenResponse>>
    {

        public string Email { get; set; }
        public string Password { get; set; }

    }
}

