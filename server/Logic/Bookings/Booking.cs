using Logic.Context;
using Logic.TimeSlots;
using Logic.Users;

namespace Logic.Bookings
{
    public class Booking : BaseEntity
    {
        public string UserEmail { get; set; }

        public User User { get; set; }

        public int TimeSlotId { get; set; }

        public TimeSlot TimeSlot { get; set; }

        public int BusinessId { get; set; }
    
    }
}