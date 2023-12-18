using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot
{
    public class DeleteTimeSlotCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }
        public string TimeSlotId { get; set; }
    }
}

