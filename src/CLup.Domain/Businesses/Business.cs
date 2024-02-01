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
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;

namespace CLup.Domain.Businesses;

public sealed class Business : Entity, IAggregateRoot
{
    private readonly List<UserMessage> _receivedMessages = new();
    private readonly List<BusinessMessage> _sentMessages = new();
    private readonly List<Employee> _employees = new();
    private readonly List<TimeSlot> _timeSlots = new();
    private readonly List<Booking> _bookings = new();

    public BusinessId Id { get; }

    public UserId OwnerId { get; private set; }

    public BusinessData BusinessData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public Interval BusinessHours { get; private set; }

    public BusinessType Type { get; private set; }

    public IReadOnlyList<UserMessage> ReceivedMessages => _receivedMessages.AsReadOnly();

    public IReadOnlyList<BusinessMessage> SentMessages => _sentMessages.AsReadOnly();

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
        Interval businessHours,
        BusinessType type)
    {
        OwnerId = ownerId;
        BusinessData = businessData;
        Address = address;
        Coords = coords;
        BusinessHours = businessHours;
        Type = type;

        Id = BusinessId.Create(Guid.NewGuid());
    }

    public Message? GetMessageById(MessageId id, bool isSender) =>
        isSender ? GetSentMessageById(id) : GetReceivedMessageById(id);

    public BusinessMessage? GetSentMessageById(MessageId id) =>
        _sentMessages.Find(message => message.Id == id);

    public UserMessage? GetReceivedMessageById(MessageId id) =>
        _receivedMessages.Find(message => message.Id == id);

    public TimeSlot? GetTimeSlotById(TimeSlotId timeSlotId) =>
        _timeSlots.Find(timeSlot => timeSlot.Id.Value == timeSlotId.Value);

    public TimeSlot? GetTimeSlotByDate(DateTime start) =>
        _timeSlots.Find(timeSlot => timeSlot.Start == start);

    public Booking? GetBookingById(BookingId bookingId) =>
        _bookings.Find(booking => booking.Id.Value == bookingId.Value);

    public Employee? GetEmployeeById(EmployeeId employeeId) =>
        _employees.Find(employee => employee.Id.Value == employeeId.Value);

    public DomainResult AddEmployee(User user, Employee employee)
    {
        if (user.Role == Role.Owner)
        {
            return DomainResult.Fail(new List<Error>() { EmployeeErrors.OwnerCannotBeEmployee });
        }

        user.UpdateRole(Role.Employee);
        _employees.Add(employee);

        return DomainResult.Ok();
    }

    public void BookingDeletedMessage(UserId receiverId)
    {
        var content = $"Your booking at {BusinessData.Name} was deleted.";
        var messageData = new MessageData("Booking Deleted", content);
        var metadata = new MessageMetadata(false, false);
        var message = new BusinessMessage(Id, receiverId, messageData, MessageType.BookingDeleted, metadata);
        _sentMessages.Add(message);
    }

    public DomainResult GenerateTimeSlots(DateOnly date)
    {
        var midnight = date.ToDateTime(TimeOnly.MinValue);
        if (GetTimeSlotByDate(midnight) != null)
        {
            return DomainResult.Fail(new List<Error>() { TimeSlotErrors.TimeSlotsExists });
        }

        var opens = midnight.AddHours(BusinessHours.Start);
        var closes = midnight.AddHours(BusinessHours.End);
        for (var curr = opens; curr.TimeOfDay <= closes.TimeOfDay; curr = curr.AddMinutes(BusinessData.TimeSlotLength))
        {
            var end = curr.AddMinutes(BusinessData.TimeSlotLength);
            var timeSlot = new TimeSlot(Id, BusinessData.Name, BusinessData.Capacity, curr, end);

            _timeSlots.Add(timeSlot);
        }

        return DomainResult.Ok();
    }
}
