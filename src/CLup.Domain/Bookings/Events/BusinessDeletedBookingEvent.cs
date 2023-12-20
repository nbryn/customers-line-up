using CLup.Domain.Businesses;
using CLup.Domain.Shared;

namespace CLup.Domain.Bookings.Events;

public sealed class BusinessDeletedBookingEvent : DomainEvent
{
    public Booking Booking { get; }

    public Business BookingOwner { get; }

    public BusinessDeletedBookingEvent(Booking booking)
    {
        BookingOwner = booking.Business;
        Booking = booking;
    }
}
