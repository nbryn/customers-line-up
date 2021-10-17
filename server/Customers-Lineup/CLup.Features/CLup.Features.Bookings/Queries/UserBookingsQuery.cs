using System.Collections.Generic;

using MediatR;

namespace CLup.Features.Bookings
{
    public class UserBookingsQuery : IRequest<IList<BookingDTO>>
    {
        public string UserId { get; set; }

        public UserBookingsQuery(string userId) => UserId = userId;
    }
}