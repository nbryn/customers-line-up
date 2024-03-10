namespace CLup.Domain.Shared.ValueObjects;

public sealed class Coords : ValueObject
{
    public double Latitude { get; }

    public double Longitude { get; }

    public Coords(double longitude, double latitude)
    {
        Guard.Against.Zero(longitude);
        Guard.Against.Zero(latitude);

        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Longitude;
        yield return Latitude;
    }
}
