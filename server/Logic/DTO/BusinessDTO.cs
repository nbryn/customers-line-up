
using Logic.Businesses;

namespace Logic.DTO
{
    public class BusinessDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public string BusinessHours { get; set; }
        public int TimeSlotLength { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

    }
}