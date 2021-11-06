using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Businesses.TimeSlots.Commands
{
    public class DeleteTimeSlotCommand : IRequest<Result>
    {

        public string Id { get; set; }

    }
}

