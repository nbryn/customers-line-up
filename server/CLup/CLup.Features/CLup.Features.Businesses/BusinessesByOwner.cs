using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Businesses
{
    public class BusinessesByOwner
    {
        public class Query : IRequest<Result<IList<BusinessDTO>>>
        {

            public string OwnerEmail { get; set; }

            public Query(string ownerEmail) => OwnerEmail = ownerEmail;
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
                var businesses = _context.Businesses.Where(x => x.OwnerEmail == query.OwnerEmail);
                var result = await _mapper.ProjectTo<BusinessDTO>(businesses).ToListAsync();

                return Result.Ok<IList<BusinessDTO>>(result);
            }
        }
    }

}

