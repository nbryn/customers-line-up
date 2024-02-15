using System;
using System.Collections.Generic;

namespace CLup.Domain.Shared.ValueObjects;

public sealed class TimeInterval : ValueObject
{
    public TimeSpan Start { get; }

    public TimeSpan End { get; }

    public TimeInterval(int startHour, int startMinutes, int endHour, int endMinutes)
    {
        if (startHour >= endHour)
        {
            throw new ArgumentOutOfRangeException(nameof(startHour));
        }
        Start = new TimeSpan(startHour, startMinutes, 0);
        End = new TimeSpan(endHour, endMinutes, 0);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}
