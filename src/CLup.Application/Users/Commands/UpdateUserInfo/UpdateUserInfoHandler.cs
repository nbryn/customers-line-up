using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Result;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo;

public sealed class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result>
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

    public async Task<Result> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
        => await _repository.FetchUserAggregate(UserId.Create(command.Id))
            .FailureIf(UserErrors.NotFound(UserId.Create(command.Id)))
            .AndThen(user => user.Update(command.Name, command.Email, Convert(command)))
            .Validate(_validator)
            .Finally(updatedUser => _repository.UpdateEntity<User, UserId>(updatedUser.Id.Value, updatedUser));

    private (Address, Coords) Convert(UpdateUserInfoCommand command)
        => (new Address(command.Street, command.Zip, command.City),
            new Coords(command.Longitude, command.Latitude));
}
