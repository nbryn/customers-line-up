using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.Users.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<UserDTO>>
    {
        private readonly IUserService _userService;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public LoginHandler(IUserService userService, CLupContext context, IMapper mapper)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserDTO>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserData.Email == command.Email)
                .ToResult()
                .Ensure(user => BC.Verify(command.Password, user.Password), (HttpCode.Unauthorized, ""))
                .AndThenF(user => _userService.DetermineRole(user))
                .Finally(user => _mapper.Map<UserDTO>(user));
        }
    }
}