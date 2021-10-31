using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Shared;

namespace CLup.Features.Businesses.Queries
{

    public class AllBusinessesHandler : IRequestHandler<AllBusinessesQuery, Result<IList<BusinessDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public AllBusinessesHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDTO>>> Handle(AllBusinessesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<BusinessDTO>(_context.Businesses).ToListAsync();

            return Result.Ok<IList<BusinessDTO>>(result);
        }
    }
}
