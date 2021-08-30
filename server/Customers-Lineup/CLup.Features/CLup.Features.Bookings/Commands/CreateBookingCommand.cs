using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Result>
    {
        public string UserEmail { get; set; }
        public string TimeSlotId { get; set; }

        public CreateBookingCommand(string userEmail, string timeSlotId)
        {
            UserEmail = userEmail;
            TimeSlotId = timeSlotId;
        }
    }
}