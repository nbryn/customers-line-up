using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Data;
using CLup.Features.Shared;
using CLup.Features.Extensions;

namespace CLup.Features.TimeSlots.Queries
{
    public class AvailableTimeSlotsByBusinessHandler : IRequestHandler<AvailableTimeSlotsByBusinessQuery, Result<List<TimeSlotDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;
        public AvailableTimeSlotsByBusinessHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<TimeSlotDTO>>> Handle(AvailableTimeSlotsByBusinessQuery query, CancellationToken cancellationToken)
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