using System.Collections.Generic;

namespace CLup.Domain.Shared.ValueObjects
{
    public class Coords : ValueObject
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coords() { }

        public Coords(double longitude, double latitude)
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