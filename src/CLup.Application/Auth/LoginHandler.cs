using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace CLup.Application.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<TokenResponse>>
    {
        private readonly IReadOnlyDbContext _readOnlyDbContext;
        private readonly IMapper _mapper;

        public LoginHandler(
            IReadOnlyDbContext readOnlyDbContext,
            IMapper mapper)
        {
            _readOnlyDbContext = readOnlyDbContext;
            _mapper = mapper;
        }

        public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
            => await _readOnlyDbContext.Users.FirstOrDefaultAsync(x => x.UserData.Email == command.Email)
                .ToResult()
                .Ensure(user => user != null && BC.Verify(command.Password, user.Password), (HttpCode.Unauthorized, ""))
                .Finally(_mapper.Map<TokenResponse>);
    }
}