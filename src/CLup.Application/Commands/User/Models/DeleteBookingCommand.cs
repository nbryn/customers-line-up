using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.User.Models
{
    public class DeleteBookingCommand : IRequest<Result>
    {
        public string BookingId { get; set; }
    }
}