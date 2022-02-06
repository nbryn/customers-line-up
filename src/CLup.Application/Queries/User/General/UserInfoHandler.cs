using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.General
{

    public class UserInfoHandler : IRequestHandler<UserInfoQuery, Result<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public UserInfoHandler(IUserService userService, IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _userService = userService;
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(UserInfoQuery query, CancellationToken cancellationToken)
        {
            return await _readOnlyContext.Users.FirstOrDefaultAsync(u => u.UserData.Email == query.Email)
                    .FailureIf("User does not exist.")
                    .AndThenF(user => _userService.DetermineRole(user))
                    .Finally(user => _mapper.Map<UserDto>(user));
        }
    }
}