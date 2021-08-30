using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Users.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<UserDTO>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public RegisterHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserDTO>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == command.Email)
                    .ToResult()
                    .EnsureDiscard(user => user == null, $"An existing user with the email '{command.Email}' was found.")
                    .AndThen(() => _mapper.Map<User>(command))
                    .AndThenF(newUser => _context.AddAndSave(newUser))
                    .Finally(newUser => _mapper.Map<UserDTO>(newUser));
        }
    }
}