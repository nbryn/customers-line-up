using CLup.Domain.Shared;

namespace CLup.Domain.Bookings
{
    public class UserDeletedBookingEvent : DomainEvent
    {
        public Booking Booking { get; }

        public UserDeletedBookingEvent(Booking booking) => Booking = booking;
    }
}