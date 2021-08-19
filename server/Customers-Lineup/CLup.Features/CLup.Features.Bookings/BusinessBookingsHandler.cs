using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Bookings.Queries;
using CLup.Features.Common;

namespace CLup.Features.Bookings
{

    public class BusinessBookingsHandler : IRequestHandler<BusinessBookingsQuery, Result<IList<BookingDTO>>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public BusinessBookingsHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<IList<BookingDTO>>> Handle(BusinessBookingsQuery query, CancellationToken cancellationToken)
        {
            var business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == query.BusinessId);

            if (business == null)
            {
                return Result.NotFound<IList<BookingDTO>>();
            }

            var bookings = _context.Bookings
                                    .Include(x => x.TimeSlot)
                                    .Include(x => x.TimeSlot.Business)
                                    .Where(x => x.BusinessId == query.BusinessId);

            var result = await _mapper.ProjectTo<BookingDTO>(bookings).ToListAsync();

            return Result.Ok<IList<BookingDTO>>(result);
        }
    }
}
