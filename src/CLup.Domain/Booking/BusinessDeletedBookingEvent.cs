using CLup.Domain.Shared;

namespace CLup.Domain.Booking
{
    public class BusinessDeletedBookingEvent : DomainEvent
    {
        public Booking Booking { get; }

        public BusinessDeletedBookingEvent(Booking booking) => Booking = booking;
    }
}