using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Application.Users;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<UserDto>>
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
        public async Task<Result<UserDto>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserData.Email == command.Email)
                .ToResult()
                .Ensure(user => BC.Verify(command.Password, user.Password), (HttpCode.Unauthorized, ""))
                .AndThenF(user => _userService.DetermineRole(user))
                .Finally(user => _mapper.Map<UserDto>(user));
        }
    }
}