using CLup.Domain.Bookings;

namespace CLup.Application.Bookings;

public sealed class BookingDto
{
    public Guid Id { get; init; }

    public Guid TimeSlotId { get; init; }

    public Guid UserId { get; init; }

    public Guid BusinessId { get; init; }

    public string Business { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }

    public string Street { get; init; }

    public string UserEmail { get; init; }

    public string Interval { get; init; }

    public string Date { get; init; }

    public string Capacity { get; init; }

    public static BookingDto FromBooking(Booking booking)
    {
        return new BookingDto()
        {
            Id = booking.Id.Value,
            UserId = booking.UserId.Value,
            BusinessId = booking.BusinessId.Value,
            TimeSlotId = booking.TimeSlotId.Value,
            Business = $"{booking.Business.BusinessData.Name} - {booking.Business.Address.Zip}",
            Date = booking.TimeSlot.Date.ToString("dd/MM/yyyy"),
            Interval = booking.TimeSlot.FormatInterval(),
            Capacity = $"{booking.TimeSlot.Bookings.Count}/{booking.TimeSlot.Capacity}",
            Latitude = booking.Business.Address.Coords.Latitude,
            Longitude = booking.Business.Address.Coords.Longitude,
            Street = booking.Business.Address.Street,
            UserEmail = booking.User.UserData.Email,
        };
    }
}
