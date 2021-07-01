using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

using CLup.Context;
using CLup.Util;

namespace CLup.TimeSlots
{
    public class TimeSlotsByBusiness
    {
        public class Query : IRequest<Result<IList<TimeSlotDTO>>>
        {
            public string BusinessId { get; set; }

            public Query() { }
            public Query(string businessId) => BusinessId = businessId;

        }
        public class Handler : IRequestHandler<Query, Result<IList<TimeSlotDTO>>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;
            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<IList<TimeSlotDTO>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var business = _context.Businesses.FirstOrDefault(b => b.Id == query.BusinessId);

                if (business == null)
                {
                    return Result.NotFound<IList<TimeSlotDTO>>();
                }

                var timeSlots = _context.TimeSlots
                                            .Include(x => x.Bookings)
                                            .Include(x => x.Business)
                                            .Where(x => x.BusinessId == query.BusinessId);

                var result = await _mapper.ProjectTo<TimeSlotDTO>(timeSlots).ToListAsync();

                return Result.Ok<IList<TimeSlotDTO>>(result);
            }
        }
    }
}

