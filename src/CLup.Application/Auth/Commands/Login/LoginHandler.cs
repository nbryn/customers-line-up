using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using CLup.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly ICLupRepository _repository;
    private readonly IAuthService _authService;

    public LoginHandler(ICLupRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserByEmail(command.Email)
            .ToResult()
            .Ensure(user => user != null && BC.Verify(command.Password, user.Password), HttpCode.Unauthorized,
                UserErrors.InvalidCredentials)
            .Finally(_authService.GenerateJwtToken);
}
