
using Logic.Businesses;

namespace Logic.DTO
{
    public class BusinessDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Zip { get; set; }
        public double Opens { get; set; }
        public double Closes { get; set; }
        public int TimeSlotLength { get; set; }
        public string Type { get; set; }

    }
}