using System;
using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Bookings.ValueObjects;

public sealed class BookingId : ValueObject
{
    public Guid Value { get; private set; }

    private BookingId(Guid value)
    {
        Value = value;
    }

    public static BookingId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}