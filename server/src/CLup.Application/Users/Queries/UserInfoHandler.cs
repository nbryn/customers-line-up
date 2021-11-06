using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Extensions;
using CLup.Application.Shared;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Queries
{

    public class UserInfoHandler : IRequestHandler<UserInfoQuery, Result<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UserInfoHandler(IUserService userService, CLupContext context, IMapper mapper)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserDto>> Handle(UserInfoQuery query, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserData.Email == query.Email)
                    .FailureIf("User does not exist.")
                    .AndThenF(user => _userService.DetermineRole(user))
                    .Finally(user => _mapper.Map<UserDto>(user));
        }
    }
}