using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo;

public sealed class UpdateUserInfoHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IValidator<User> _validator;
    private readonly ICLupRepository _repository;

    public UpdateUserInfoHandler(
        IValidator<User> validator,
        ICLupRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public async Task<Result> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(command.UserId)
            .FailureIfNotFound(UserErrors.NotFound)
            .AndThen(user => user.Update(command.Name, command.Email, Convert(command)))
            .Validate(_validator)
            .Finally(updatedUser => _repository.UpdateEntity(updatedUser.Id.Value, updatedUser));

    private (Address, Coords) Convert(UpdateUserCommand command)
        => (new Address(command.Street, command.Zip, command.City),
            new Coords(command.Longitude, command.Latitude));
}
