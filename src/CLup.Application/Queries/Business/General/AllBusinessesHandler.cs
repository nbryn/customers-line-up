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
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public AllBusinessesHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _readOnlyContext = readOnlyContext;
            _mapper = mapper;
        }

        public async Task<Result<IList<BusinessDto>>> Handle(AllBusinessesQuery query, CancellationToken cancellationToken)
        {
            var result = await _mapper.ProjectTo<BusinessDto>(_readOnlyContext.Businesses).ToListAsync();

            return Result.Ok<IList<BusinessDto>>(result);
        }
    }
}
