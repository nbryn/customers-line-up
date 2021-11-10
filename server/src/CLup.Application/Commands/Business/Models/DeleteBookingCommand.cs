using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.Business.Models
{
    public class DeleteBookingCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }

        public string BookingId { get; set; }
        
        public string BusinessId { get; set; }

        public DeleteBookingCommand(
            string ownerEmail,
            string bookingId,
            string businessId)
        {
            OwnerEmail = ownerEmail;
            BookingId = bookingId;
            BusinessId = businessId;
        }
    }
}