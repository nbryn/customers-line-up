using CLup.Application.TimeSlots.Commands.DeleteTimeSlot;
using CLup.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using BId = CLup.Domain.Businesses.ValueObjects.BusinessId;
using TId = CLup.Domain.TimeSlots.ValueObjects.TimeSlotId;

namespace CLup.API.Contracts.TimeSlots.DeleteTimeSlot;

public sealed class DeleteTimeSlotRequest()
{
    [FromRoute]
    public Guid TimeSlotId { get; set; }

    [FromQuery]
    public Guid BusinessId { get; set; }

    public DeleteTimeSlotCommand MapToCommand(UserId userId) =>
        new(userId, BId.Create(BusinessId), TId.Create(TimeSlotId));
}
