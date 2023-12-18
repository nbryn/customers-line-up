using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Shared.ValueObjects;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Commands.UpdateUserInfo
{
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result>
    {
        private readonly IValidator<Domain.Users.User> _validator;
        private readonly ICLupRepository _repository;

        public UpdateUserInfoHandler(
            IValidator<Domain.Users.User> validator,
            ICLupRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
            => await _repository.FetchUserAggregate(command.Id)
                .FailureIf("User not found.")
                .AndThen(user => user.Update(command.Name, command.Email, Convert(command)))
                .Validate(_validator)
                .Finally(updatedUser => _repository.UpdateEntity(updatedUser.Id, updatedUser));

        private (Address, Coords) Convert(UpdateUserInfoCommand command)
            => (new Address(command.Street, command.Zip, command.City),
                new Coords(command.Longitude, command.Latitude));
    }
}