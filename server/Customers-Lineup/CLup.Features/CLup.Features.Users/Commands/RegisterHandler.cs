using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Users.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserDTO>>
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
        public async Task<Result<UserDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserData.Email == command.Email)
                    .ToResult()
                    .EnsureDiscard(user => user == null, $"The email '{command.Email}' is already in use.")
                    .AndThen(() => _mapper.Map<User>(command))
                    .Validate(_validator)
                    .AndThenF(newUser => _context.AddAndSave(newUser))
                    .Finally(newUser => _mapper.Map<UserDTO>(newUser));
        }
    }
}