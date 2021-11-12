using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.User.CreateBooking
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