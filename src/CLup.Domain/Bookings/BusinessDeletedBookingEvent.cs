using CLup.Domain.Businesses;
using CLup.Domain.Shared;

namespace CLup.Domain.Bookings
{
    public class BusinessDeletedBookingEvent : DomainEvent
    {
        public Business Business { get; }

        public string ReceiverId { get; }

        public BusinessDeletedBookingEvent(Business business, string receiverId)
        {
            Business = business;
            ReceiverId = receiverId;
        }
    }
}