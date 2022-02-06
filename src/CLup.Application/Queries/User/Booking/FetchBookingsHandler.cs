using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Queries.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.Booking
{
    
    public class FetchBookingsHandler : IRequestHandler<FetchBookingsQuery, IList<BookingDto>>
    {
        private readonly IReadOnlyDbContext _readOnlyContext;
        private readonly IMapper _mapper;

        public FetchBookingsHandler(IReadOnlyDbContext readOnlyContext, IMapper mapper)
        {
            _mapper = mapper;
            _readOnlyContext = readOnlyContext;
        }

        public async Task<IList<BookingDto>> Handle(FetchBookingsQuery query, CancellationToken cancellationToken)
        {
            // Check access -> Email matches user id requested
            var bookings = await _readOnlyContext.Bookings
                                    .Include(x => x.Business)
                                    .Include(x => x.TimeSlot)
                                    .ThenInclude(x => x.Business)
                                    .Where(x => x.UserId == query.UserId)
                                    .OrderBy(x => x.TimeSlot.Start)
                                    .ToListAsync();

            return bookings.Select(_mapper.Map<BookingDto>).ToList();
        }
    }
}