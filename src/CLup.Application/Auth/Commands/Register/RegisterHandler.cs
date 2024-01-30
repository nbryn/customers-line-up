using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth.Commands.Register;

public sealed class RegisterHandler : IRequestHandler<RegisterCommand, Result<string>>
{
    private readonly IValidator<User> _validator;
    private readonly ICLupRepository _repository;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterHandler(
        IValidator<User> validator,
        ICLupRepository repository,
        IJwtTokenService jwtTokenService)
    {
        _validator = validator;
        _repository = repository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Result<string>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserByEmail(command.UserData.Email)
            .ToResult()
            .Ensure(user => user == null, HttpCode.BadRequest, UserErrors.EmailExists(command.UserData.Email))
            .AndThen(_ => command.MapToUser(BC.HashPassword(command.UserData.Password)))
            .Validate(_validator)
            .AndThenF(newUser => _repository.AddAndSave(cancellationToken, newUser))
            .Finally(_jwtTokenService.GenerateJwtToken);
}
