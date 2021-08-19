using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

using CLup.Data;
using CLup.Features.Businesses.Queries;
using CLup.Features.Common;

namespace CLup.Features.Businesses
{
    public class BusinessesByOwnerHandler : IRequestHandler<BusinessesByOwnerQuery, Result<IList<BusinessDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BusinessesByOwnerHandler(CLupContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDTO>>> Handle(BusinessesByOwnerQuery query, CancellationToken cancellationToken)
        {
            var businesses = _context.Businesses.Where(x => x.OwnerEmail == query.OwnerEmail);
            var result = await _mapper.ProjectTo<BusinessDTO>(businesses).ToListAsync();

            return Result.Ok<IList<BusinessDTO>>(result);
        }
    }
}