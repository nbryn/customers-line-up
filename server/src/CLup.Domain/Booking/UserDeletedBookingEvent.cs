using MediatR;

namespace CLup.Domain.Booking
{
    public class UserDeletedBookingEvent : INotification
    {
        public Booking Booking { get; }

        public UserDeletedBookingEvent(Booking booking) => Booking = booking;
    }
}