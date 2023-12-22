using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;
using CLup.Application.Shared.Result;

namespace CLup.Application.Auth.Commands.Register;

public sealed class RegisterHandler : IRequestHandler<RegisterCommand, Result<TokenResponse>>
{
    private readonly IValidator<User> _validator;
    private readonly ICLupRepository _repository;
    private readonly IMapper _mapper;

    public RegisterHandler(
        IValidator<User> validator,
        ICLupRepository repository,
        IMapper mapper)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<TokenResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(null, command.Email)
            .ToResult()
            .Ensure(user => user == null, HttpCode.BadRequest, UserErrors.EmailExists(command.Email))
            .AndThen(_ => _mapper.Map<User>(command))
            .Validate(_validator)
            .AndThenF(newUser => _repository.AddAndSave(newUser))
            .Finally(_mapper.Map<TokenResponse>);
}
