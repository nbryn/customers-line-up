namespace CLup.Users.DTO
{
    public record UserInsightsDTO
    {
        public int Bookings { get; set; }
        public int Businesses { get; set; }
        public string NextBookingBusiness { get; set; }
        public string NextBookingTime { get; set; }
    }
}