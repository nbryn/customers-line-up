using System;
using System.Collections.Generic;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users.ValueObjects;
using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Domain.Businesses;

using System.Linq;
using Bookings;
using Bookings.ValueObjects;
using Employees;

public sealed class Business : Entity<BusinessId>, IAggregateRoot
{
    private readonly List<Message> _receivedMessages = new();
    private readonly List<Message> _sentMessages = new();
    private readonly List<Employee> _employees = new();
    private readonly List<TimeSlot> _timeSlots = new();
    private readonly List<Booking> _bookings = new();

    public UserId OwnerId { get; private set; }

    public BusinessData BusinessData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public TimeSpan BusinessHours { get; private set; }

    public BusinessType Type { get; private set; }

    public IReadOnlyList<Message> ReceivedMessages => _receivedMessages.AsReadOnly();

    public IReadOnlyList<Message> SentMessages => _sentMessages.AsReadOnly();

    public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

    public IReadOnlyList<TimeSlot> TimeSlots => _timeSlots.AsReadOnly();

    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

    protected Business()
    {
    }

    public Business(
        UserId ownerId,
        BusinessData businessData,
        Address address,
        Coords coords,
        TimeSpan businessHours,
        BusinessType type)
    {
        OwnerId = ownerId;
        BusinessData = businessData;
        Address = address;
        Coords = coords;
        BusinessHours = businessHours;
        Type = type;
    }

    public string Opens => BusinessHours.Start;

    public string Closes => BusinessHours.End;

    public string Name => BusinessData.Name;

    public Booking GetBooking(BookingId bookingId) =>
        _bookings.Find(booking => booking.Id.Value == bookingId.Value);

    public void BookingDeletedMessage(Guid receiverId)
    {
        var content = $"Your booking at {Name} was deleted.";
        var messageData = new MessageData("Booking Deleted", content);
        var metadata = new MessageMetadata(false, false);
        var message = new Message(Id.Value, receiverId, messageData, MessageType.BookingDeleted, metadata);
        _sentMessages.Add(message);
    }

    public IList<TimeSlot> GenerateTimeSlots(DateTime start)
    {
        var opens = start.AddHours(double.Parse(Opens.Substring(0, Opens.IndexOf("."))));
        var closes = start.AddHours(double.Parse(Closes.Substring(0, Closes.IndexOf("."))));

        var timeSlots = new List<TimeSlot>();
        for (var date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(BusinessData.TimeSlotLength))
        {
            var end = date.AddMinutes(BusinessData.TimeSlotLength);
            var timeSlot = new TimeSlot(Id, Name, BusinessData.Capacity, date, end);

            timeSlots.Add(timeSlot);
        }

        return timeSlots;
    }
}
