using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.Bookings
{
    public class BusinessDeletedBookingEvent : DomainEvent
    {
        public User Owner { get; }

        public string BusinessId { get; }

        public string ReceiverId { get; }

        public BusinessDeletedBookingEvent(
            User owner, 
            string businessId,
            string receiverId)
        {
            Owner = owner;
            BusinessId = businessId;
            ReceiverId = receiverId;
        }
    }
}