using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Bookings.Commands
{
    public class UserDeleteBookingCommand : IRequest<Result>
    {

        public string BookingId { get; set; }
    }
}