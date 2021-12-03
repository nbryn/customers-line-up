using System.Collections.Generic;
using CLup.Application.Queries.Shared;
using MediatR;

namespace CLup.Application.Queries.User.Booking
{
    public class FetchBookingsQuery : IRequest<IList<BookingDto>>
    {
        public string UserId { get; set; }
    }
}