namespace CLup.Domain
{
    public class Booking : BaseEntity
    {
        public string UserId { get; private set; }

        public User User { get; private set; }

        public string TimeSlotId { get; private set; }

        public TimeSlot TimeSlot { get; private set; }

        public string BusinessId { get; private set; }

        public Business Business { get; private set; }

        public Booking(string userId, string timeSlotId, string businessId)
            : base()
        {
            UserId = userId;
            TimeSlotId = timeSlotId;
            BusinessId = businessId;
        }
    }
}