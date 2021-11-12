using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.General
{

    public class AllBusinessesHandler : IRequestHandler<AllBusinessesQuery, Result<IList<BusinessDto>>>
    {
        private readonly IQueryDbContext _queryContext;
        private readonly IMapper _mapper;

        public AllBusinessesHandler(IQueryDbContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(AllBusinessesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<BusinessDto>(_queryContext.Businesses).ToListAsync();

            return Result.Ok<IList<BusinessDto>>(result);
        }
    }
}
