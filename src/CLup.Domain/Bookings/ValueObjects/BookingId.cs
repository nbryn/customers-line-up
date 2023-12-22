using System;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Bookings.ValueObjects;

public sealed class BookingId : Id
{
    private BookingId(Guid value)
    {
        Value = value;
    }

    public static BookingId Create(Guid value) => new(value);
}
