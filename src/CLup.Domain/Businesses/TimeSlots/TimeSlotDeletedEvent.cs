using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.Businesses.TimeSlots
{
    public class TimeSlotDeletedEvent : DomainEvent
    {
        public User BusinessOwner { get; }

        public TimeSlot TimeSlot { get; }

        public TimeSlotDeletedEvent(
            User businessOwner,
            TimeSlot timeSlot)
        {
            BusinessOwner = businessOwner;
            TimeSlot = timeSlot;
        }
    }
}