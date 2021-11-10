using System.Collections.Generic;
using System.Linq;
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
    public class BusinessesByOwnerHandler : IRequestHandler<BusinessesByOwnerQuery, Result<IList<BusinessDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BusinessesByOwnerHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(BusinessesByOwnerQuery query, CancellationToken cancellationToken)
        {
            var businesses = _context.Businesses.Where(x => x.OwnerEmail == query.OwnerEmail);
            var result = await _mapper.ProjectTo<BusinessDto>(businesses).ToListAsync();

            return Result.Ok<IList<BusinessDto>>(result);
        }
    }
}