using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.TimeSlots.Events;

public sealed class TimeSlotDeletedEvent : DomainEvent
{
    public User BusinessOwner { get; }

    public TimeSlot TimeSlot { get; }

    public TimeSlotDeletedEvent(TimeSlot timeSlot, User businessOwner)
    {
        BusinessOwner = businessOwner;
        TimeSlot = timeSlot;
    }
}