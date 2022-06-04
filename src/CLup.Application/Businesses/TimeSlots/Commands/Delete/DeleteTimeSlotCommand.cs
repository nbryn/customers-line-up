using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands.Delete
{
    public class DeleteTimeSlotCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }
        public string TimeSlotId { get; set; }
    }
}

