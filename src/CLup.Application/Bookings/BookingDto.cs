using System;

namespace CLup.Application.Bookings;

public sealed class BookingDto
{
    public Guid Id { get; set; }

    public Guid TimeSlotId { get; set; }

    public Guid UserId { get; set; }

    public Guid BusinessId { get; set; }

    public string Business { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string Street { get; set; }

    public string UserEmail { get; set; }

    public string Interval { get; set; }

    public string Date { get; set; }

    public string Capacity { get; set; }
}
