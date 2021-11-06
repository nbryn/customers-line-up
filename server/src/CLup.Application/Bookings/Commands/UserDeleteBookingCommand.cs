using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Bookings.Commands
{
    public class UserDeleteBookingCommand : IRequest<Result>
    {

        public string BookingId { get; set; }
    }
}