using System;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.TimeSlots.ValueObjects;

public sealed class TimeSlotId : Id
{
    private TimeSlotId(Guid value)
    {
        Value = value;
    }

    public static TimeSlotId Create(Guid value) => new(value);

}
