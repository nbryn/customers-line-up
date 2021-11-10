namespace CLup.Application.Shared.Models
{
    public class TimeSlotDto
    {
        public string Id { get; set; }
        
        public string BusinessId { get; set; }
        public string Business { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        
        public string End { get; set; }
        public string Interval { get; set; }
        public string Capacity { get; set; }
    }
}