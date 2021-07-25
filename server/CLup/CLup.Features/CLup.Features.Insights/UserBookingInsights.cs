using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Features.Common;

namespace CLup.Features.Insights
{
    public class UserBookingInsights
    {
        public class Query : IRequest<Result<Model>>
        {
            public string UserEmail { get; set; }

            public Query(string userEmail) => UserEmail = userEmail;
        }

        public class Model
        {
            public int OwnBookings { get; set; }
            public string NextBookingBusiness { get; set; }
            public string NextBookingTime { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Model>>
        {
            private readonly CLupContext _context;
            private readonly IMapper _mapper;

            public Handler(CLupContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Model>> Handle(Query query, CancellationToken cancellationToken)
            {
                var bookings = await _context.Bookings
                                        .Include(b => b.Business)
                                        .Include(b => b.TimeSlot)
                                        .Where(x => x.UserEmail == query.UserEmail)
                                        .ToListAsync();


                var nextBooking = bookings.OrderBy(x => Math.Abs(x.TimeSlot.Start.Ticks - DateTime.Now.Ticks)).First();

                var insights = new Model
                {
                    OwnBookings = bookings.Count,
                    NextBookingBusiness = nextBooking.Business.Name,
                    NextBookingTime = nextBooking.TimeSlot.Start.ToString("dd/MM/yyyy - HH:mm")
                };

                return Result.Ok<Model>(insights);
            }
        }
    }
}

