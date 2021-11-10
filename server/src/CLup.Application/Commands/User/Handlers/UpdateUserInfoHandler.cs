using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Commands.User.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Domain.Shared.ValueObjects;
using CLup.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Commands.User.Handlers
{
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result>
    {
        private readonly IValidator<Domain.User.User> _validator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UpdateUserInfoHandler(
            IValidator<Domain.User.User> validator,
            CLupContext context, 
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == command.Id)
                    .FailureIf($"User with the email '{command.Email}' was not found.")
                    .AndThen((user) => user.Update(command.Name, command.Email, Convert(command)))
                    .Validate(_validator)
                    .Finally(updatedUser => _context.UpdateEntity(updatedUser.Id, updatedUser));
        }

        private (Address, Coords) Convert(UpdateUserInfoCommand command)    
           => (new Address(command.Street, command.Zip, command.City), new Coords(command.Longitude, command.Latitude));      
    }
}