using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users.Queries
{
    public class Handler : IRequestHandler<FetchAllUsersQuery, Result<IList<UserDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public Handler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<IList<UserDto>>> Handle(FetchAllUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<UserDto>(_context.Users).ToListAsync();

            return Result.Ok<IList<UserDto>>(result);
        }
    }
}