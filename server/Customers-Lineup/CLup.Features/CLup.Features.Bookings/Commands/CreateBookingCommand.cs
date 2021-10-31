using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {

        public string UserId { get; set; }

        public string TimeSlotId { get; set; }
        
        public string BusinessId { get; set; }

        public CreateBookingCommand(string userId, string timeSlotId, string businessId)
        {
            UserId = userId;   
            TimeSlotId = timeSlotId; 
            BusinessId = businessId;
        }
    }
}