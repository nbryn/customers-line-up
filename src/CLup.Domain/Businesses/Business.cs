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

using Bookings;
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

    public IReadOnlyList<Message> ReceivedMessages => this._receivedMessages.AsReadOnly();

    public IReadOnlyList<Message> SentMessages => this._sentMessages.AsReadOnly();

    public IReadOnlyList<Employee> Employees => this._employees.AsReadOnly();

    public IReadOnlyList<TimeSlot> TimeSlots => this._timeSlots.AsReadOnly();

    public IReadOnlyList<Booking> Bookings => this._bookings.AsReadOnly();

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
        this.OwnerId = ownerId;
        this.BusinessData = businessData;
        this.Address = address;
        this.Coords = coords;
        this.BusinessHours = businessHours;
        this.Type = type;
    }

    public string Opens => this.BusinessHours.Start;

    public string Closes => this.BusinessHours.End;

    public string Name => this.BusinessData.Name;

    public void BookingDeletedMessage(Guid receiverId)
    {
        var content = $"Your booking at {this.Name} was deleted.";
        var messageData = new MessageData("Booking Deleted", content);
        var metadata = new MessageMetadata(false, false);
        var message = new Message(this.Id.Value, receiverId, messageData, MessageType.BookingDeleted, metadata);
        this._sentMessages.Add(message);
    }

    public IList<TimeSlot> GenerateTimeSlots(DateTime start)
    {
        var opens = start.AddHours(double.Parse(this.Opens.Substring(0, this.Opens.IndexOf("."))));
        var closes = start.AddHours(double.Parse(this.Closes.Substring(0, this.Closes.IndexOf("."))));

        var timeSlots = new List<TimeSlot>();
        for (var date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(this.BusinessData.TimeSlotLength))
        {
            var end = date.AddMinutes(this.BusinessData.TimeSlotLength);
            var timeSlot = new TimeSlot(this.Id, this.Name, this.BusinessData.Capacity, date, end);

            timeSlots.Add(timeSlot);
        }

        return timeSlots;
    }
}
