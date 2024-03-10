using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Bookings.ValueObjects;

public sealed class BookingId : Id
{
    private BookingId(Guid value) : base(value)
    {
    }

    public static BookingId Create(Guid value) => new(value);
}
