using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using CLup.Domain.Users;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth.Commands.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    private readonly ICLupRepository _repository;
    private readonly IMapper _mapper;

    public LoginHandler(
        ICLupRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregateById(command.Id)
            .ToResult()
            .Ensure(user => user != null && BC.Verify(command.Password, user.Password), HttpCode.Unauthorized,
                UserErrors.InvalidCredentials)
            .Finally(_mapper.Map<TokenResponse>);
}
