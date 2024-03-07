namespace CLup.Domain.Shared.ValueObjects;

public abstract class Id : ValueObject
{
    public Guid Value { get; protected init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
