using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.Business.TimeSlot.Models;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Models;
using CLup.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.TimeSlot.Handlers
{
    public class Handler : IRequestHandler<TimeSlotsByBusinessQuery, Result<List<TimeSlotDto>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;
        public Handler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<Result<List<TimeSlotDto>>> Handle(TimeSlotsByBusinessQuery query, CancellationToken cancellationToken)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .FailureIfDiscard("Business not found.")
                    .AndThen(() => _context.TimeSlots
                                        .Include(x => x.Bookings)
                                        .Include(x => x.Business)
                                        .Where(x => x.BusinessId == query.BusinessId)
                                        .OrderBy(x => x.Start))

                    .Finally(timeSlots => _mapper.ProjectTo<TimeSlotDto>(timeSlots).ToListAsync());
        }
    }
}