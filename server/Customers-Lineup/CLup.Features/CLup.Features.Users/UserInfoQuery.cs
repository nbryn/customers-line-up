using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.Users
{
    public class UserInfoQuery
    {
        public class Query : IRequest<Result<UserDTO>>
        {
            public string Email { get; set; }
            
            public Query(string email) => Email = email;
        }

        public class Handler : IRequestHandler<Query, Result<UserDTO>>
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
            public async Task<Result<UserDTO>> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == query.Email)
                        .FailureIf("User does not exist.")
                        .AndThenF(user => _userService.DetermineRole(user))
                        .Finally(user => _mapper.Map<UserDTO>(user));
            }
        }
    }
}

