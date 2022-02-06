using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.Business.TimeSlot.Available
{
    public class AvailableTimeSlotsByBusinessHandler : IRequestHandler<AvailableTimeSlotsByBusinessQuery, Result<List<TimeSlotDto>>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;
        public AvailableTimeSlotsByBusinessHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<Result<List<TimeSlotDto>>> Handle(AvailableTimeSlotsByBusinessQuery query, CancellationToken cancellationToken)
        {
            return await _readOnlyContext.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId)
                    .FailureIfDiscard("Business not found.")
                    .AndThen(() => _readOnlyContext.TimeSlots
                                        .Include(x => x.Bookings)
                                        .Include(x => x.Business)
                                        .Where(x => x.BusinessId == query.BusinessId)
                                        .OrderBy(x => x.Start))

                    .Finally(timeSlots => _mapper.ProjectTo<TimeSlotDto>(timeSlots).ToListAsync());
        }
    }
}