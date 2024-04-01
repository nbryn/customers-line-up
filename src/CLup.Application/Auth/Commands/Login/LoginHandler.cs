using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly ICLupRepository _repository;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginHandler(ICLupRepository repository, IJwtTokenService jwtTokenService)
    {
        _repository = repository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<string>> Handle(LoginCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserByEmail(command.Email)
            .BadRequestIfNotFound(UserErrors.InvalidCredentials)
            .Ensure(user => user != null && BC.Verify(command.Password, user.UserData.Password), HttpCode.Unauthorized,
                UserErrors.InvalidCredentials)
            .Finally(_jwtTokenService.GenerateJwtToken);
}
