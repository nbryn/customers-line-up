using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Bookings.Queries
{
    public class BusinessBookingsQuery : IRequest<Result<List<BookingDto>>>
    {
        public string BusinessId { get; set; }
    }
}