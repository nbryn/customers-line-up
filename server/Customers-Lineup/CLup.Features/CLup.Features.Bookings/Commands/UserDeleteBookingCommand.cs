using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Bookings.Commands
{
    public class UserDeleteBookingCommand : IRequest<Result>
    {

        public string BookingId { get; set; }
    }
}