using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.Business.TimeSlot.Delete
{
    public class DeleteTimeSlotCommand : IRequest<Result>
    {

        public string Id { get; set; }

    }
}

