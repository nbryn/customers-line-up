using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Users.Bookings.Commands.Delete
{
    public class UserDeleteBookingCommand : IRequest<Result>
    {
        public string UserEmail { get; set; }
        
        public string BookingId { get; set; }
    }
}