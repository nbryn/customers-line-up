using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.Business.TimeSlot.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.TimeSlot.Handlers
{
    public class Handler : IRequestHandler<TimeSlotsByBusinessQuery, Result<List<TimeSlotDto>>>
    {
        private readonly IQueryDbContext _queryContext;
        private readonly IMapper _mapper;
        public Handler(IQueryDbContext queryContext, IMapper mapper)
        {
            _mapper = mapper;
            _queryContext = queryContext;
        }
        public async Task<Result<List<TimeSlotDto>>> Handle(TimeSlotsByBusinessQuery query, CancellationToken cancellationToken)
        {
            return await _queryContext.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .FailureIfDiscard("Business not found.")
                    .AndThen(() => _queryContext.TimeSlots
                                        .Include(x => x.Bookings)
                                        .Include(x => x.Business)
                                        .Where(x => x.BusinessId == query.BusinessId)
                                        .OrderBy(x => x.Start))

                    .Finally(timeSlots => _mapper.ProjectTo<TimeSlotDto>(timeSlots).ToListAsync());
        }
    }
}