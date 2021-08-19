using System.Collections.Generic;

using MediatR;

namespace CLup.Features.Bookings
{
    public class UserBookingsQuery : IRequest<IList<BookingDTO>>
    {
        public string UserEmail { get; set; }

        public UserBookingsQuery(string userEmail) => UserEmail = userEmail;
    }
}