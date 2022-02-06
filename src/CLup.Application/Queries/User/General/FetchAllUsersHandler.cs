using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.General
{
    public class Handler : IRequestHandler<FetchAllUsersQuery, Result<IList<UserDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public Handler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }
        public async Task<Result<IList<UserDto>>> Handle(FetchAllUsersQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<UserDto>(_readOnlyContext.Users).ToListAsync();

            return Result.Ok<IList<UserDto>>(result);
        }
    }
}