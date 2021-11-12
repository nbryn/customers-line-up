using CLup.Domain.Shared;

namespace CLup.Domain.Business.TimeSlot
{
    public class TimeSlotDeletedEvent : DomainEvent
    {
        public TimeSlot TimeSlot { get; }

        public TimeSlotDeletedEvent(TimeSlot timeSlot) => TimeSlot = timeSlot;
    }
}