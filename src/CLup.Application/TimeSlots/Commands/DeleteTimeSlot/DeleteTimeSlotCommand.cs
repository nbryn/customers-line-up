using CLup.Application.Shared;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class DeleteTimeSlotCommand : IRequest<Result>
{
    public UserId OwnerId { get; }

    public BusinessId BusinessId { get; }

    public TimeSlotId TimeSlotId { get; }

    public DeleteTimeSlotCommand(UserId ownerId, BusinessId businessId, TimeSlotId timeSlotId)
    {
        OwnerId = ownerId;
        BusinessId = businessId;
        TimeSlotId = timeSlotId;
    }
}
