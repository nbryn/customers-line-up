using MediatR;

namespace CLup.Domain.Bookings
{
    public class BusinessDeletedBookingEvent : INotification
    {
        public Booking Booking { get; }

        public BusinessDeletedBookingEvent(Booking booking) => Booking = booking;
    }
}