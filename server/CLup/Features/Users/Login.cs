using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Auth;
using CLup.Context;
using CLup.Users.Interfaces;
using CLup.Util;

namespace CLup.Users
{
    public class Login
    {
        public class Command : IRequest<Result<UserDTO>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<UserDTO>>
        {
            private readonly IAuthService _authService;
            private readonly IUserService _userService;
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(IAuthService authService, IUserService userService, CLupContext context, IMapper mapper)
            {
                _authService = authService;
                _userService = userService;
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<UserDTO>> Handle(Command command, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

                if (user == null || !BC.Verify(command.Password, user.Password))
                {
                    return Result.Unauthorized<UserDTO>();
                }

                var token = _authService.GenerateJWTToken(command.Email);

                await _userService.DetermineRole(user);
                
                var response = _mapper.Map<UserDTO>(user);
                response.Token = token;

                return Result.Ok<UserDTO>(response);
            }
        }
    }
}

