using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Employees.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;

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

    public TimeInterval BusinessHours { get; private set; }

    public BusinessType Type { get; private set; }

    public IReadOnlyList<UserMessage> ReceivedMessages => _receivedMessages.AsReadOnly();

    public IReadOnlyList<BusinessMessage> SentMessages => _sentMessages.AsReadOnly();

    public IReadOnlyList<Employee> Employees => _employees.AsReadOnly();

    public IReadOnlyList<TimeSlot> TimeSlots => _timeSlots.AsReadOnly();

    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

    public Business(
        UserId ownerId,
        BusinessData businessData,
        Address address,
        TimeInterval businessHours,
        BusinessType type)
    {
        Guard.Against.Null(ownerId);
        Guard.Against.Null(businessData);
        Guard.Against.Null(address);
        Guard.Against.Null(businessHours);
        Guard.Against.EnumOutOfRange(type);

        OwnerId = ownerId;
        BusinessData = businessData;
        Address = address;
        BusinessHours = businessHours;
        Type = type;

        Id = BusinessId.Create(Guid.NewGuid());
    }

    private Business()
    {
    }

    public Message? GetMessageById(MessageId messageId, bool receivedMessage)
        => receivedMessage
            ? GetReceivedMessageById(messageId)
            : GetSentMessageById(messageId);

    public BusinessMessage? GetSentMessageById(MessageId id) =>
        _sentMessages.Find(message => message.Id.Value == id.Value);

    public UserMessage? GetReceivedMessageById(MessageId id) =>
        _receivedMessages.Find(message => message.Id.Value == id.Value);

    public TimeSlot? GetTimeSlotById(TimeSlotId timeSlotId) =>
        _timeSlots.Find(timeSlot => timeSlot.Id.Value == timeSlotId.Value);

    public bool TimeSlotsGeneratedOnDate(DateOnly date) =>
        _timeSlots.Exists(timeSlot => timeSlot.Date == date);

    public Booking? GetBookingById(BookingId bookingId) =>
        _bookings.Find(booking => booking.Id.Value == bookingId.Value);

    public Employee? GetEmployeeById(EmployeeId employeeId) =>
        _employees.Find(employee => employee.Id.Value == employeeId.Value);

    public TimeSlot RemoveTimeSlot(TimeSlot timeSlot)
    {
        _timeSlots.Remove(timeSlot);
        return timeSlot;
    }

    public Employee RemoveEmployee(Employee employee)
    {
        _employees.Remove(employee);
        return employee;
    }

    public Business Update(
        BusinessData businessData,
        Address address,
        TimeInterval businessHours,
        BusinessType type)
    {
        BusinessData = businessData;
        Address = address;
        BusinessHours = businessHours;
        Type = type;

        return this;
    }

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
        if (TimeSlotsGeneratedOnDate(date))
        {
            return DomainResult.Fail(new List<Error>() { TimeSlotErrors.Exists });
        }

        var midnight = TimeOnly.FromDateTime(date.ToDateTime(TimeOnly.MinValue));
        var opens = midnight.Add(BusinessHours.Start.ToTimeSpan());
        var closes = midnight.Add(BusinessHours.End.ToTimeSpan());
        for (var curr = opens;
             curr.AddMinutes(BusinessData.TimeSlotLengthInMinutes) <= closes;
             curr = curr.AddMinutes(BusinessData.TimeSlotLengthInMinutes))
        {
            var end = curr.AddMinutes(BusinessData.TimeSlotLengthInMinutes);
            var interval = new TimeInterval(curr, end);
            var timeSlot = new TimeSlot(Id, BusinessData.Name, BusinessData.Capacity, date, interval);

            _timeSlots.Add(timeSlot);
        }

        return DomainResult.Ok();
    }
}
