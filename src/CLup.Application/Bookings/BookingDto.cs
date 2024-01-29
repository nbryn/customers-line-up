using System;
using CLup.Domain.Bookings;
using CLup.Domain.TimeSlots;

namespace CLup.Application.Bookings;

public sealed class BookingDto
{
    public Guid Id { get; private set; }

    public Guid TimeSlotId { get; private set; }

    public Guid UserId { get; private set; }

    public Guid BusinessId { get; private set; }

    public string Business { get; private set; }

    public double Longitude { get; private set; }

    public double Latitude { get; private set; }

    public string Street { get; private set; }

    public string UserEmail { get; private set; }

    public string Interval { get; private set; }

    public string Date { get; private set; }

    public string Capacity { get; private set; }

    public BookingDto FromBooking(Booking booking)
    {
        Id = booking.Id.Value;
        UserId = booking.UserId.Value;
        BusinessId = booking.BusinessId.Value;
        TimeSlotId = booking.TimeSlotId.Value;
        Business = $"{booking.Business.BusinessData.Name} - {booking.Business.Address.Zip}";
        Date = booking.TimeSlot.Start.ToString("dd/MM/yyyy");
        Interval = booking.TimeSlot.FormatInterval();
        Capacity = $"{booking.TimeSlot.Bookings.Count}/{booking.TimeSlot.Capacity}";
        Latitude = booking.Business.Coords.Latitude;
        Longitude = booking.Business.Coords.Longitude;
        Street = booking.Business.Address.Street;
        UserEmail = booking.User.UserData.Email;

        return this;
    }
}
