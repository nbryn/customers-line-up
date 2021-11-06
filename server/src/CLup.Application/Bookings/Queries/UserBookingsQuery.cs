using System.Collections.Generic;
using MediatR;

namespace CLup.Application.Bookings.Queries
{
    public class UserBookingsQuery : IRequest<IList<BookingDto>>
    {
        public string UserId { get; set; }
    }
}