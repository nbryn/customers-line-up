using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.TimeSlots.ValueObjects;

public sealed class TimeSlotId : ValueObject
{
    public Guid Value { get; private set; }

    private TimeSlotId(Guid value)
    {
        Value = value;
    }

    public static TimeSlotId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}