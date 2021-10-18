using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {

        public string TimeSlotId { get; set; }
        public string UserId { get; set; }
        

        public CreateBookingCommand(string timeSlotId, string userId)
        {
            TimeSlotId = timeSlotId;
            UserId = userId;       
        }
    }
}