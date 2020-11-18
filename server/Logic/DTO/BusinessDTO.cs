
using Logic.Businesses;

namespace Logic.DTO
{
    public class BusinessDTO
    {
        public string Name { get; set; }
        public string Zip { get; set; }
        public double OpeningTime { get; set; }

        public double ClosingTime { get; set; }

        public string Type { get; set; }

    }
}