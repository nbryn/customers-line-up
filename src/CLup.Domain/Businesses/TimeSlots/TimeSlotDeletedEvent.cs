using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.Businesses.TimeSlots
{
    public class TimeSlotDeletedEvent : DomainEvent
    {
        public User BusinessOwner { get; }

        public TimeSlot TimeSlot { get; }

        public TimeSlotDeletedEvent(TimeSlot timeSlot)
        {
            BusinessOwner = timeSlot.Business.Owner;
            TimeSlot = timeSlot;
        }
    }
}