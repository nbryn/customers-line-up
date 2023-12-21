using CLup.Domain.Shared;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.TimeSlots.Events;

public sealed class TimeSlotDeletedEvent : DomainEvent
{
    public TimeSlot TimeSlot { get; }

    public TimeSlotDeletedEvent(TimeSlot timeSlot)
    {
        TimeSlot = timeSlot;
    }
}
