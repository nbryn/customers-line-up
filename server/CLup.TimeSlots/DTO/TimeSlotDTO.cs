namespace CLup.TimeSlots.DTO
{
    public class TimeSlotDTO
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string Business { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Capacity { get; set; }
    }
}