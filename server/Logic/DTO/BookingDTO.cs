namespace Logic.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public int TimeSlotId { get; set; }

        public string Business { get; set; }

        public string Address { get; set; }

        public string UserMail { get; set; }

        public string Interval { get; set; }

        public string Date { get; set; }

        public string Capacity { get; set; }

    }
}