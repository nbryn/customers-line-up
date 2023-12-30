using System;
using System.Collections.Generic;

namespace CLup.Domain.Shared.ValueObjects;

public sealed class Interval : ValueObject
{
    public double Start { get; private set; }

    public double End { get; private set; }

    public Interval() { }

    public Interval(double start, double end)
    {
        Start = start;
        End = end;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
