namespace CLup.Domain.Shared.ValueObjects;

public abstract class Id : ValueObject
{
    public Guid Value { get; protected init; }

    protected Id(Guid value)
    {
        Guard.Against.NullOrEmpty(value);
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
