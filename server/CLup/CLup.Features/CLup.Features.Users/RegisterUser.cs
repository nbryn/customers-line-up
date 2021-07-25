using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Users
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
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<UserDTO>> Handle(Command command, CancellationToken cancellationToken)
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email == command.Email)
                        .ToResult()
                        .EnsureDiscard(user => user == null, $"An existing user with the email '{command.Email}' was found.")
                        .AndThen(() => _mapper.Map<User>(command))
                        .AndThenF(newUser => _context.AddAndSave(newUser))
                        .AndThen(newUser => _mapper.Map<UserDTO>(newUser));
            }
        }
    }
}

