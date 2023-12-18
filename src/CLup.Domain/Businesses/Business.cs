using System;
using System.Collections.Generic;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Domain.Businesses;

public sealed class Business : Entity<BusinessId>, IAggregateRoot
{
    private readonly List<MessageId> _receivedMessageIds = new();
    private readonly List<MessageId> _sentMessageIds = new();
    private readonly List<EmployeeId> _employeeIds = new();
    private readonly List<TimeSlotId> _timeSlotIds = new();
    private readonly List<BookingId> _bookingIds = new();

    public UserId OwnerId { get; private set; }

    public BusinessData BusinessData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public TimeSpan BusinessHours { get; private set; }

    public BusinessType Type { get; private set; }

    public IReadOnlyList<MessageId> ReceivedMessageIds => _receivedMessageIds.AsReadOnly();

    public IReadOnlyList<MessageId> SentMessageIds => _sentMessageIds.AsReadOnly();

    public IReadOnlyList<EmployeeId> EmployeeIds => _employeeIds.AsReadOnly();

    public IReadOnlyList<TimeSlotId> TimeSlotIds => _timeSlotIds.AsReadOnly();

    public IReadOnlyList<BookingId> BookingIds => _bookingIds.AsReadOnly();

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

    public void BookingDeletedMessage(Guid receiverId)
    {
        var content = $"Your booking at {Name} was deleted.";
        var messageData = new MessageData("Booking Deleted", content);
        var metadata = new MessageMetadata(false, false);
        var message = new Message(Id.Value, receiverId, messageData, MessageType.BookingDeleted, metadata);
        _sentMessageIds.Add(message.Id);
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