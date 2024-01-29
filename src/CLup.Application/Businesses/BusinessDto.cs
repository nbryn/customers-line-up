using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Application.Bookings;
using CLup.Application.Employees;
using CLup.Application.Messages;
using CLup.Application.TimeSlots;
using CLup.Domain.Businesses;

namespace CLup.Application.Businesses;

public sealed class BusinessDto
{
    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; }

    public string Zip { get; private set; }

    public string City { get; private set; }

    public string Street { get; private set; }

    public double Longitude { get; private set; }

    public double Latitude { get; private set; }

    public string Opens { get; private set; }

    public string Closes { get; private set; }

    public int TimeSlotLength { get; private set; }

    public string BusinessHours { get; private set; }

    public string Type { get; private set; }

    public int Capacity { get; private set; }

    public IList<BookingDto> Bookings { get; private set; }

    public IList<EmployeeDto> Employees { get; private set; }

    public IList<MessageDto> ReceivedMessages { get; private set; }

    public IList<MessageDto> SentMessages { get; private set; }

    public IList<TimeSlotDto> TimeSlots { get; private set; }

    public BusinessDto FromBusiness(Business business)
    {
        Id = business.Id.Value;
        OwnerId = business.OwnerId.Value;
        Name = business.BusinessData.Name;
        Zip = business.Address.Zip;
        City = business.Address.City;
        Street = business.Address.Street;
        Longitude = business.Coords.Longitude;
        Latitude = business.Coords.Latitude;
        Opens = business.BusinessHours.Start.ToString();
        Closes = business.BusinessHours.End.ToString();
        Capacity = business.BusinessData.Capacity;
        TimeSlotLength = business.BusinessData.TimeSlotLength;
        Type = business.Type.ToString();
        BusinessHours = $"{Opens} - {Closes}";

        Bookings = business.Bookings.Select(new BookingDto().FromBooking).ToList();
        Employees = business.Employees.Select(new EmployeeDto().FromEmployee).ToList();
        ReceivedMessages = business.ReceivedMessages.Select(new MessageDto().FromMessage).ToList();
        SentMessages = business.SentMessages.Select(new MessageDto().FromMessage).ToList();
        TimeSlots = business.TimeSlots.Select(new TimeSlotDto().FromTimeSlot).ToList();

        return this;
    }
}
