using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CLup.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Bookings.Queries
{
    
    public class UserBookingsHandler : IRequestHandler<UserBookingsQuery, IList<BookingDto>>
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;

        public UserBookingsHandler(CLupContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IList<BookingDto>> Handle(UserBookingsQuery query, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings
                                    .Include(x => x.TimeSlot)
                                    .Include(x => x.TimeSlot.Business)
                                    .Where(x => x.UserId == query.UserId)
                                    .OrderBy(x => x.TimeSlot.Start)
                                    .AsNoTracking()
                                    .ToListAsync();

            return bookings.Select(_mapper.Map<BookingDto>).ToList();
        }
    }
}