namespace CLup.Application.Shared.Models
{
    public class BookingDto
    {
        public string Id { get; set; }

        public string TimeSlotId { get; set; }

        public string Business { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Street { get; set; }

        public string UserEmail { get; set; }

        public string Interval { get; set; }

        public string Date { get; set; }

        public string Capacity { get; set; }

    }
}