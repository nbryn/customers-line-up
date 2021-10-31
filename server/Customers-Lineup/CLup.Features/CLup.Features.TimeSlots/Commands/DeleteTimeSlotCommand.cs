using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.TimeSlots.Commands
{
    public class DeleteTimeSlotCommand : IRequest<Result>
    {

        public string Id { get; set; }

    }
}

