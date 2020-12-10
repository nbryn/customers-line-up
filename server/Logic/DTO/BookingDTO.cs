namespace Logic.DTO
{
    public class BookingDTO
    {
        public int TimeSlotId { get; set; }

        public int BusinessId { get; set; }

        public string UserMail { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int NumberOfUsersWithSameBooking { get; set; }

    }
}