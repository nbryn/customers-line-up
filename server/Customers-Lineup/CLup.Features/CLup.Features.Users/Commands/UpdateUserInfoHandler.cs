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
    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, Result<UserDTO>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UpdateUserInfoHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserDTO>> Handle(UpdateUserInfoCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == command.Id)
                    .ToResult()
                    .EnsureDiscard(user => user != null, $"User with the email '{command.Email}' was not found.")
                    .AndThen(() => _mapper.Map<User>(command))
                    .AndThenF(updatedUser => _context.UpdateEntity<User>(updatedUser.Id, updatedUser))
                    .Finally(updatedUser => _mapper.Map<UserDTO>(updatedUser));
        }
    }
}