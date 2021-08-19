using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Users.Queries;

namespace CLup.Features.Users
{
    public class Handler : IRequestHandler<FetchAllUsersQuery, Result<IList<UserDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public Handler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<IList<UserDTO>>> Handle(FetchAllUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<UserDTO>(_context.Users).ToListAsync();

            return Result.Ok<IList<UserDTO>>(result);
        }
    }
}