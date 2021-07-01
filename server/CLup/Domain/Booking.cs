using CLup.Context;
using CLup.Businesses;
using CLup.TimeSlots;
using CLup.Users;

namespace CLup.Bookings
{
    public class Booking : BaseEntity
    {
        public string UserEmail { get; set; }

        public User User { get; set; }

        public string TimeSlotId { get; set; }

        public TimeSlot TimeSlot { get; set; }

        public string BusinessId { get; set; }

        public Business Business { get; set; }

    }
}