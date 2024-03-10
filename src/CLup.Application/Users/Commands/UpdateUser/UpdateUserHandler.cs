using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using FluentValidation;

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
            .AndThen(user => user!.Update(command.Address, command.Name, command.Email))
            .Validate(_validator)
            .FinallyAsync(_ => _repository.SaveChangesAsync(true, cancellationToken));
}
