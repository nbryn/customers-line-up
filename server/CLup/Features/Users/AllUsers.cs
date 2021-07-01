using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Context;
using CLup.Util;

namespace CLup.Users
{
    public class AllUsers
    {
        public class Query : IRequest<Result<IList<UserDTO>>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<IList<UserDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<IList<UserDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync();

                return Result.Ok<IList<UserDTO>>(result);
            }
        }
    }
}

