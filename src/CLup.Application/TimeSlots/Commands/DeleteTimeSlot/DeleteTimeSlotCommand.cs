using System;
using CLup.Application.Shared.Result;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotCommand : IRequest<Result>
{
    public Guid OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid TimeSlotId { get; set; }
}
