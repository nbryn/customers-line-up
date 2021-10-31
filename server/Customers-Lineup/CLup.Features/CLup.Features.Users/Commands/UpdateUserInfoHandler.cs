using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Domain.ValueObjects;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Users.Commands
{
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result>
    {
        private readonly IValidator<User> _validator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UpdateUserInfoHandler(
            IValidator<User> validator,
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
                    .AndThen((user) => user.Update(user.Email, user.Name, Convert(command)))
                    .Validate(_validator)
                    .Finally(updatedUser => _context.UpdateEntity(updatedUser.Id, updatedUser));
        }

        private (Address, Coords) Convert(UpdateUserInfoCommand command)    
           => (new Address(command.Street, command.Zip, command.City), new Coords(command.Latitude, command.Longitude));      
    }
}