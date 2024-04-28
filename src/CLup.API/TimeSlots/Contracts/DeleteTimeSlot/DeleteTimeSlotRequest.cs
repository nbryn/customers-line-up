using CLup.Application.TimeSlots.Commands.DeleteTimeSlot;
using CLup.Domain.Users.ValueObjects;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using TId = CLup.Domain.TimeSlots.ValueObjects.TimeSlotId;

namespace CLup.API.TimeSlots.Contracts.DeleteTimeSlot;

public readonly record struct DeleteTimeSlotRequest(Guid TimeSlotId, Guid BusinessId)
{
    public DeleteTimeSlotCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), TId.Create(TimeSlotId));
}
