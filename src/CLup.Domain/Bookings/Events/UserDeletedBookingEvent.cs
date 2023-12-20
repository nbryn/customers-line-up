using CLup.Domain.Shared;
using CLup.Domain.Users;

namespace CLup.Domain.Bookings.Events;

public sealed class UserDeletedBookingEvent : DomainEvent
{
    public Booking Booking { get; }

    public User Owner { get; }

    public UserDeletedBookingEvent(Booking booking)
    {
        Booking = booking;
        Owner = booking.User;
    }
}
