using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Application.Bookings;
using CLup.Application.Employees;
using CLup.Application.Messages;
using CLup.Application.TimeSlots;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;

namespace CLup.Application.Businesses;

public sealed class BusinessDto
{
    public Guid Id { get; init; }

    public Guid OwnerId { get; init; }

    public string Name { get; init; }

    public string Zip { get; init; }

    public string City { get; init; }

    public string Street { get; init; }

    public double Longitude { get; init; }

    public double Latitude { get; init; }

    public string Opens { get; init; }

    public string Closes { get; init; }

    public int TimeSlotLength { get; init; }

    public string BusinessHours { get; init; }

    public BusinessType Type { get; init; }

    public int Capacity { get; init; }

    public IList<BookingDto> Bookings { get; init; }

    public IList<EmployeeDto> Employees { get; init; }

    public IList<MessageDto> ReceivedMessages { get; init; }

    public IList<MessageDto> SentMessages { get; init; }

    public IList<TimeSlotDto> TimeSlots { get; init; }

    public static BusinessDto FromBusiness(Business business)
    {
        return new BusinessDto()
        {
            Id = business.Id.Value,
            OwnerId = business.OwnerId.Value,
            Name = business.BusinessData.Name,
            Zip = business.Address.Zip,
            City = business.Address.City,
            Street = business.Address.Street,
            Longitude = business.Coords.Longitude,
            Latitude = business.Coords.Latitude,
            Opens = business.BusinessHours.Start.ToString(),
            Closes = business.BusinessHours.End.ToString(),
            Capacity = business.BusinessData.Capacity,
            TimeSlotLength = business.BusinessData.TimeSlotLengthInMinutes,
            Type = business.Type,
            BusinessHours = $"{business.BusinessHours.Start.ToString()} - {business.BusinessHours.End.ToString()}",
            Bookings = business.Bookings.Select(BookingDto.FromBooking).ToList(),
            Employees = business.Employees.Select(EmployeeDto.FromEmployee).ToList(),
            TimeSlots = business.TimeSlots.Select(TimeSlotDto.FromTimeSlot).ToList(),

            ReceivedMessages = business.ReceivedMessages
                .Where(message => !message.Metadata.DeletedByReceiver)
                .Select(MessageDto.FromMessage)
                .ToList(),

            SentMessages = business.SentMessages
                .Where(message => !message.Metadata.DeletedBySender)
                .Select(MessageDto.FromMessage)
                .ToList(),
        };
    }
}
