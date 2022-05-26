using CLup.Domain.Businesses;
using CLup.Domain.Shared;

namespace CLup.Domain.Bookings
{
    public class BusinessDeletedBookingEvent : DomainEvent
    {
        public Booking Booking { get; }

        public Business Business { get; }

        public BusinessDeletedBookingEvent(Booking booking)
        {
            Business = booking.Business;
            Booking = booking;
        }
    }
}