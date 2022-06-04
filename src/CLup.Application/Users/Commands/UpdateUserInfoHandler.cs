using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Shared.ValueObjects;
using FluentValidation;
using MediatR;

namespace CLup.Application.Users.Commands
{
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result>
    {
        private readonly IValidator<Domain.Users.User> _validator;
        private readonly ICLupDbContext _context;

        public UpdateUserInfoHandler(
            IValidator<Domain.Users.User> validator,
            ICLupDbContext context)
        {
            _validator = validator;
            _context = context;
        }

        public async Task<Result> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.Id)
                .FailureIf("User not found.")
                .AndThen(user => user.Update(command.Name, command.Email, Convert(command)))
                .Validate(_validator)
                .Finally(updatedUser => _context.UpdateEntity(updatedUser.Id, updatedUser));

        private (Address, Coords) Convert(UpdateUserInfoCommand command)
            => (new Address(command.Street, command.Zip, command.City),
                new Coords(command.Longitude, command.Latitude));
    }
}