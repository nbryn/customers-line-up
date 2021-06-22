using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Bookings.DTO;
using CLup.Context;

namespace CLup.Bookings
{
    public class UserBookings
    {
        public class Query : IRequest<IList<BookingDTO>>
        {
            public string UserEmail { get; set; }

            public Query(string userEmail) => UserEmail = userEmail;

        }

        public class Handler : IRequestHandler<Query, IList<BookingDTO>>
        {
            private readonly CLupContext _context;

            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<IList<BookingDTO>> Handle(Query query, CancellationToken cancellationToken)
            {
                var bookings = _context.Bookings
                                        .Include(x => x.TimeSlot)
                                        .Include(x => x.TimeSlot.Business)
                                        .Where(x => x.UserEmail == query.UserEmail);

                return await _mapper.ProjectTo<BookingDTO>(bookings).ToListAsync();
            }
        }
    }
}