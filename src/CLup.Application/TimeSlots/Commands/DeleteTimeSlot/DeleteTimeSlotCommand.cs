using System;
using CLup.Application.Shared;
using CLup.Domain.Users.ValueObjects;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotCommand : IRequest<Result>
{
    public UserId OwnerId { get; set; }

    public Guid BusinessId { get; set; }

    public Guid TimeSlotId { get; set; }

    public DeleteTimeSlotCommand(UserId ownerId, Guid businessId, Guid timeSlotId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        TimeSlotId = timeSlotId;
    }
}
