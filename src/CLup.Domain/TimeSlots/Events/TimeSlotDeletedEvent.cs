using CLup.Domain.Shared;

namespace CLup.Domain.TimeSlots.Events;

public sealed class TimeSlotDeletedEvent : DomainEvent
{
    public TimeSlot TimeSlot { get; }

    public TimeSlotDeletedEvent(TimeSlot timeSlot)
    {
        TimeSlot = timeSlot;
    }
}
