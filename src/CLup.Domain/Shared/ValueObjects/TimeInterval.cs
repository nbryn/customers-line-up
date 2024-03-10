namespace CLup.Domain.Shared.ValueObjects;

public sealed class TimeInterval : ValueObject
{
    public TimeOnly Start { get; }

    public TimeOnly End { get; }

    public TimeInterval(TimeOnly start, TimeOnly end)
    {
        Guard.Against.Null(start);
        Guard.Against.Null(end);

        if (start >= end)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (start.Second != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        if (end.Second != 0)
        {
            throw new ArgumentOutOfRangeException(nameof(end));
        }

        Start = start;
        End = end;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
