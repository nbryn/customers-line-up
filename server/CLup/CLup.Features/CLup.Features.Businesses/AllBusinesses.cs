using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Businesses
{
    public class AllBusinesses
    {
        public class Query : IRequest<Result<IList<BusinessDTO>>>
        {

        }
        public class Handler : IRequestHandler<Query, Result<IList<BusinessDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<IList<BusinessDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var result = await _mapper.ProjectTo<BusinessDTO>(_context.Businesses).ToListAsync();

                return Result.Ok<IList<BusinessDTO>>(result);
            }
        }
    }
}

