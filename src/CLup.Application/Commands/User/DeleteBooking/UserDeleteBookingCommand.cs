using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.User.DeleteBooking
{
    public class UserDeleteBookingCommand : IRequest<Result>
    {
        public string BookingId { get; set; }
    }
}