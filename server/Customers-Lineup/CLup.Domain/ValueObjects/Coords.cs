using System.Collections.Generic;

namespace CLup.Domain.ValueObjects
{
    public class Coords : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coords() { }

        public Coords(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return Longitude;
            yield return Latitude;
        }
    }
}