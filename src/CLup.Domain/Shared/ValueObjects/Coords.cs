using System.Collections.Generic;

namespace CLup.Domain.Shared.ValueObjects;

public sealed class Coords : ValueObject
{
    public double Latitude { get; private set; }

    public double Longitude { get; private set; }

    public Coords()
    {

    }

    public Coords(double longitude, double latitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Longitude;
        yield return Latitude;
    }
}