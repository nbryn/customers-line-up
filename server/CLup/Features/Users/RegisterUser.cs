using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Auth;
using CLup.Context;
using CLup.Util;

namespace CLup.Users
{
    public class RegisterUser
    {
        public class Command : IRequest<Result<UserDTO>>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Zip { get; set; }
            public string Address { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Zip).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<UserDTO>>
        {
            private readonly IAuthService _authService;
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(IAuthService authService, CLupContext context, IMapper mapper)
            {
                _authService = authService;
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<UserDTO>> Handle(Command command, CancellationToken cancellationToken)
            {
                var userExists = await _context.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

                if (userExists != null)
                {
                    return Result.Conflict<UserDTO>($"An existing user with the email '{command.Email}' was found.");
                }

                string token = _authService.GenerateJWTToken(command.Email);
                command.Password = BC.HashPassword(command.Password);

                var newUser = _mapper.Map<User>(command);
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                var response = _mapper.Map<UserDTO>(newUser);

                return Result.Created<UserDTO>(response);
            }
        }
    }
}

