using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.Bookings
{
    public class UserDeletedBookingEvent : DomainEvent
    {
        public Booking Booking { get; }

        public User User { get; }

        public UserDeletedBookingEvent(Booking booking)
        {
            Booking = booking;
            User = booking.User;
        }
    }
}