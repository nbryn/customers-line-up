using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string TimeSlotId { get; set; }

        public CreateBookingCommand(string userId, string timeSlotId)
        {
            UserId = userId;
            TimeSlotId = timeSlotId;
        }
    }
}