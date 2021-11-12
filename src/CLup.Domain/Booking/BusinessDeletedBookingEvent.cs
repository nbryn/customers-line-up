using CLup.Domain.Shared;

namespace CLup.Domain.Booking
{
    public class BusinessDeletedBookingEvent : DomainEvent
    {
        public Business.Business Business { get; }

        public string ReceiverId { get; }

        public BusinessDeletedBookingEvent(Business.Business business, string receiverId)
        {
            Business = business;
            ReceiverId = receiverId;
        }
    }
}