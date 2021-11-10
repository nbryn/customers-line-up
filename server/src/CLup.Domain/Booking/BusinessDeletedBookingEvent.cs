using MediatR;

namespace CLup.Domain.Booking
{
    public class BusinessDeletedBookingEvent : INotification
    {
        public Booking Booking { get; }

        public BusinessDeletedBookingEvent(Booking booking) => Booking = booking;
    }
}