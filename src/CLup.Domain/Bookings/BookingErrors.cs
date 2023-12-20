namespace CLup.Domain.Bookings;

using Shared;

public class BookingErrors
{
    public static Error NotFound() =>
        new("Bookings.NotFound", "The booking was not found.");
}
