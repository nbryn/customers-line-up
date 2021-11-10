using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Application.Queries.User.Models;
using CLup.Application.Shared.Models;
using CLup.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User.Handlers
{
    
    public class FetchBookingsHandler : IRequestHandler<FetchBookingsQuery, IList<BookingDto>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public FetchBookingsHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<BookingDto>> Handle(FetchBookingsQuery query, CancellationToken cancellationToken)
        {
            // Check access -> Email matches user id requested
            var bookings = await _context.Bookings
                                    .Include(x => x.Business)
                                    .Include(x => x.TimeSlot)
                                    .ThenInclude(x => x.Business)
                                    .Where(x => x.UserId == query.UserId)
                                    .OrderBy(x => x.TimeSlot.Start)
                                    .AsNoTracking()
                                    .ToListAsync();

            return bookings.Select(_mapper.Map<BookingDto>).ToList();
        }
    }
}