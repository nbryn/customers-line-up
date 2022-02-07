using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.Bookings.Commands.Delete
{
    public class BusinessDeleteBookingCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }

        public string BookingId { get; set; }
        
        public string BusinessId { get; set; }

        public BusinessDeleteBookingCommand(
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