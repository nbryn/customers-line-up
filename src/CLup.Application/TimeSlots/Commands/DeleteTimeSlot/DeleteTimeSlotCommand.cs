using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot
{
    using Shared.Result;

    public class DeleteTimeSlotCommand : IRequest<Result>
    {
        public string OwnerEmail { get; set; }
        public string TimeSlotId { get; set; }
    }
}

