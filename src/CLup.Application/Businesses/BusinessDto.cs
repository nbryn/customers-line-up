using System;
using System.Collections.Generic;
using CLup.Application.Bookings;
using CLup.Application.Employees;
using CLup.Application.Messages;
using CLup.Application.TimeSlots;

namespace CLup.Application.Businesses;

public sealed class BusinessDto
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public string Name { get; set; }

    public string Zip { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public double Longitude { get; set; }

    public double Latitude { get; set; }

    public string Opens { get; set; }

    public string Closes { get; set; }

    public int TimeSlotLength { get; set; }

    public string BusinessHours { get; set; }

    public string Type { get; set; }

    public int Capacity { get; set; }

    public IList<BookingDto> Bookings { get; set; }

    public IList<EmployeeDto> Employees { get; set; }

    public IList<MessageDto> Messages { get; set; }

    public IList<TimeSlotDto> TimeSlots { get; set; }
}
