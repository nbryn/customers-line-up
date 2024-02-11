using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IValidator<User> _validator;
    private readonly ICLupRepository _repository;

    public UpdateUserHandler(
        IValidator<User> validator,
        ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.UserId)
            .FailureIfNotFound(UserErrors.NotFound)
            .AndThen(user => user.Update(command.UserData, command.Address, command.Coords))
            .Validate(_validator)
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
