using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.Business.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using CLup.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.Handlers
{

    public class AllBusinessesHandler : IRequestHandler<AllBusinessesQuery, Result<IList<BusinessDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public AllBusinessesHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(AllBusinessesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<BusinessDto>(_context.Businesses).ToListAsync();

            return Result.Ok<IList<BusinessDto>>(result);
        }
    }
}
