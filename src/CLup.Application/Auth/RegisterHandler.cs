using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.Users;
using FluentValidation;
using MediatR;

namespace CLup.Application.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<TokenResponse>>
    {
        private readonly IValidator<User> _validator;
        private readonly ICLupDbContext _context;
        private readonly IMapper _mapper;

        public RegisterHandler(
            IValidator<User> validator,
            ICLupDbContext context,
            IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<TokenResponse>> Handle(RegisterCommand command, CancellationToken cancellationToken)
            => await _context.FetchUserAggregate(command.Email)
                .ToResult()
                .EnsureDiscard(user => user == null, $"The email '{command.Email}' is already in use.")
                .AndThen(() => _mapper.Map<User>(command))
                .Validate(_validator)
                .AndThenF(newUser => _context.AddAndSave(newUser))
                .Finally(_mapper.Map<TokenResponse>);
    }
}