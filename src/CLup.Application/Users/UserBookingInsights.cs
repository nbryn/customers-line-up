using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared;
using CLup.Application.Shared.Extensions;
using CLup.Application.Shared.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Users
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
            private readonly IReadOnlyDbContext _readOnlyContext;

            public Handler(IReadOnlyDbContext readOnlyContext) => _readOnlyContext = readOnlyContext;
            
            public async Task<Result<Model>> Handle(Query query, CancellationToken cancellationToken)
            {

                return await _readOnlyContext.Bookings.Include(b => b.Business)
                        .Include(b => b.TimeSlot)
                        .Include(b => b.User)
                        .Where(x => x.User.UserData.Email == query.UserEmail)
                        .ToListAsync()
                        .ToResult()
                        .AndThenDouble(bookings => bookings?.OrderBy(x => Math.Abs(x.TimeSlot.Start.Ticks - DateTime.Now.Ticks)).FirstOrDefault())
                        .Finally((bookings, nextBooking) => new Model
                        {
                            OwnBookings = bookings?.Count ?? 0,
                            NextBookingBusiness = nextBooking?.Business.BusinessData.Name ?? "You don't have any bookings.",
                            NextBookingTime = nextBooking?.TimeSlot.Start.ToString("dd/MM/yyyy - HH:mm") ?? "You don't have any bookings."
                        });
            }
        }
    }
}

