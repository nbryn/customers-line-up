using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth.Commands.Login;

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Shared.Result;

public class LoginHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
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
        => await _repository.FetchUserAggregate(command.Email)
            .ToResult()
            .Ensure(user => user != null && BC.Verify(command.Password, user.Password), string.Empty, HttpCode.Unauthorized)
            .Finally(_mapper.Map<TokenResponse>);
}
