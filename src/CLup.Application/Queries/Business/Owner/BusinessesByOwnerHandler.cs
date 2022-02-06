using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.Owner
{
    public class BusinessesByOwnerHandler : IRequestHandler<BusinessesByOwnerQuery, Result<IList<BusinessDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public BusinessesByOwnerHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(BusinessesByOwnerQuery query, CancellationToken cancellationToken)
        {
            var businesses = _readOnlyContext.Businesses.Where(x => x.OwnerEmail == query.OwnerEmail);
            var result = await _mapper.ProjectTo<BusinessDto>(businesses).ToListAsync();

            return Result.Ok<IList<BusinessDto>>(result);
        }
    }
}