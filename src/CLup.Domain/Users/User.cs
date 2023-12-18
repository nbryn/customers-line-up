using System;
using System.Collections.Generic;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Users;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    private readonly List<MessageId> _receivedMessageIds = new();
    private readonly List<MessageId> _sentMessageIds = new();
    private readonly List<BusinessId> _businessIds = new();
    private readonly List<BookingId> _bookingIds = new();

    public UserData UserData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public Role Role { get; set; }

    public IReadOnlyList<MessageId> ReceivedMessageIds => _receivedMessageIds.AsReadOnly();

    public IReadOnlyList<MessageId> SentMessageIds => _sentMessageIds.AsReadOnly();

    public IReadOnlyList<BusinessId> BusinessIds => _businessIds.AsReadOnly();

    public IReadOnlyList<BookingId> BookingIds => _bookingIds.AsReadOnly();

    protected User()
    {
    }

    public User(
        UserData userData,
        Address address,
        Coords coords,
        Role role)
    {
        UserData = userData;
        Address = address;
        Coords = coords;
        Role = role;
    }

    public string Name => UserData.Name;

    public string Email => UserData.Email;

    public string Password => UserData.Password;

    public bool IsBusinessOwner => BusinessIds?.Count > 0;

    public User UpdateRole(Role role)
    {
        Role = role;

        return this;
    }

    public User Update(string name, string email, (Address address, Coords coords) info)
    {
        UserData = new UserData(name, email, Password);
        Address = info.address;
        Coords = info.coords;

        return this;
    }

    public void UserDeletedBookingMessage(Business business, TimeSlot timeSlot, Guid receiverId)
    {
        var content =
            $"The user with email {Email} deleted her/his booking at {timeSlot.Start.ToString("dd/MM/yyyy")}.";
        var messageData = new MessageData($"Booking Deleted - {business.Name}", content);
        var metaData = new MessageMetadata(false, false);
        var message = new Message(Id.Value, receiverId, messageData, MessageType.BookingDeleted, metaData);
        _sentMessageIds.Add(message.Id);
    }
}