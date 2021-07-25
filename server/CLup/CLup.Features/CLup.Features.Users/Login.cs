using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Users
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
            private readonly IUserService _userService;
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(IUserService userService, CLupContext context, IMapper mapper)
            {
                _userService = userService;
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<UserDTO>> Handle(Command command, CancellationToken cancellationToken)
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email == command.Email)
                    .FailureIf()
                    .Ensure(user => BC.Verify(command.Password, user.Password), (HttpCode.Unauthorized, ""))
                    .AndThenF(user => _userService.DetermineRole(user))
                    .AndThen(user => _mapper.Map<UserDTO>(user));
            }
        }
    }
}

