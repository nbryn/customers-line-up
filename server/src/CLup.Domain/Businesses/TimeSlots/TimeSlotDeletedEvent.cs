
using MediatR;

namespace CLup.Domain.Businesses.TimeSlots
{
    public class TimeSlotDeletedEvent : INotification
    {
        public TimeSlot TimeSlot { get; }

        public TimeSlotDeletedEvent(TimeSlot timeSlot) => TimeSlot = timeSlot;
    }
}