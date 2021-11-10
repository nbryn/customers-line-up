
using MediatR;

namespace CLup.Domain.Business.TimeSlot
{
    public class TimeSlotDeletedEvent : INotification
    {
        public TimeSlot TimeSlot { get; }

        public TimeSlotDeletedEvent(TimeSlot timeSlot) => TimeSlot = timeSlot;
    }
}