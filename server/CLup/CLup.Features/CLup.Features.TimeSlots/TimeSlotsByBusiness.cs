using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Common;
using CLup.Features.Extensions;

namespace CLup.Features.TimeSlots
{
    public class TimeSlotsByBusiness
    {
        public class Query : IRequest<Result<List<TimeSlotDTO>>>
        {
            public string BusinessId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<TimeSlotDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;
            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<List<TimeSlotDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                        .FailureIfDiscard("Business not found.")
                        .AndThen(() => _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .Include(x => x.Business)
                                            .Where(x => x.BusinessId == query.BusinessId)
                                            .OrderBy(x => x.Start))

                        .Finally(timeSlots => _mapper.ProjectTo<TimeSlotDTO>(timeSlots).ToListAsync());
            }
        }
    }
}

