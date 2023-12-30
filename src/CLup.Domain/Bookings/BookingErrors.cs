using CLup.Domain.Shared;

namespace CLup.Domain.Bookings;

public static class BookingErrors
{
    public static Error NotFound => new("Bookings.NotFound", "The booking was not found.");
}
