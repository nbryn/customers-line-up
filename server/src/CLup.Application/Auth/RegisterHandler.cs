using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.User;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using CLup.Domain.User;
using CLup.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserDto>>
    {
        private readonly IValidator<User> _validator;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public RegisterHandler(
            IValidator<User> validator,
            CLupContext context, 
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserData.Email == command.Email)
                    .ToResult()
                    .EnsureDiscard(user => user == null, $"The email '{command.Email}' is already in use.")
                    .AndThen(() => _mapper.Map<User>(command))
                    .Validate(_validator)
                    .AndThenF(newUser => _context.AddAndSave(newUser))
                    .Finally(newUser => _mapper.Map<UserDto>(newUser));
        }
    }
}