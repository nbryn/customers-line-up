using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Businesses.ValueObjects;

public sealed class BusinessData : ValueObject
{
    public string Name { get; }

    public int Capacity { get; }

    public int TimeSlotLengthInMinutes { get; }

    public BusinessData(string name, int capacity, int timeSlotLengthInMinutes)
    {
        Name = name;
        Capacity = capacity;
        TimeSlotLengthInMinutes = timeSlotLengthInMinutes;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Capacity;
        yield return TimeSlotLengthInMinutes;
    }
}
