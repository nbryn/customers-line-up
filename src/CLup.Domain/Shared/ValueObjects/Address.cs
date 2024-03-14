namespace CLup.Domain.Shared.ValueObjects;

public sealed class Address : ValueObject
{
    public string Street { get; }

    public int Zip { get; }

    public string City { get; }

    public Coords Coords { get; }

    public Address(string street, int zip, string city, Coords coords)
    {
        Guard.Against.NullOrWhiteSpace(street);
        Guard.Against.NegativeOrZero(zip);
        Guard.Against.NullOrWhiteSpace(city);
        Guard.Against.Null(coords);

        Street = street;
        Zip = zip;
        City = city;
        Coords = coords;
    }

    private Address()
    {
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return Zip;
        yield return City;
    }
}
