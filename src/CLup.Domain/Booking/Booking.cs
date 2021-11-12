using System.Collections.Generic;
using CLup.Domain.Business.TimeSlot;
using CLup.Domain.Shared;

namespace CLup.Domain.Booking
{
    public class Booking : Entity, IHasDomainEvent
    {
        public string UserId { get; private set; }

        public User.User User { get; private set; }

        public string TimeSlotId { get; private set; }

        public TimeSlot TimeSlot { get; private set; }

        public string BusinessId { get; private set; }

        public Business.Business Business { get; private set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        public Booking(string userId, string timeSlotId, string businessId)
            : base()
        {
            UserId = userId;
            TimeSlotId = timeSlotId;
            BusinessId = businessId;
        }
    }
}