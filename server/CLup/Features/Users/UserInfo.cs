using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Users.Interfaces;
using CLup.Util;

namespace CLup.Users
{
    public class UserInfo
    {
        public class Query : IRequest<Result>
        {
            public string Email { get; set; }
            
            public Query(string email) => Email = email;
        }

        public class Handler : IRequestHandler<Query, Result>
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
            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == query.Email);

                if (user == null)
                {
                    return Result.NotFound();
                }

                await _userService.DetermineRole(user);
                var result = _mapper.Map<UserDTO>(user);

                return Result.Ok<UserDTO>(result);
            }
        }
    }
}

