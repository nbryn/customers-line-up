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

using Bookings;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    private readonly List<Message> _receivedMessages = new();
    private readonly List<BusinessId> _businessIds = new();
    private readonly List<Message> _sentMessage = new();
    private readonly List<Booking> _bookings = new();

    public UserData UserData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public Role Role { get; set; }

    public IReadOnlyList<Message> ReceivedMessages => this._receivedMessages.AsReadOnly();

    public IReadOnlyList<Message> SentMessages => this._sentMessage.AsReadOnly();

    public IReadOnlyList<BusinessId> BusinessIds => this._businessIds.AsReadOnly();

    public IReadOnlyList<Booking> Bookings => this._bookings.AsReadOnly();

    protected User()
    {
    }

    public User(
        UserData userData,
        Address address,
        Coords coords,
        Role role)
    {
        this.UserData = userData;
        this.Address = address;
        this.Coords = coords;
        this.Role = role;
    }

    public string Name => this.UserData.Name;

    public string Email => this.UserData.Email;

    public string Password => this.UserData.Password;

    public bool IsBusinessOwner => this.BusinessIds?.Count > 0;

    public User UpdateRole(Role role)
    {
        this.Role = role;

        return this;
    }

    public User Update(string name, string email, (Address address, Coords coords) info)
    {
        this.UserData = new UserData(name, email, this.Password);
        this.Address = info.address;
        this.Coords = info.coords;

        return this;
    }

    public void UserDeletedBookingMessage(Business business, TimeSlot timeSlot, Guid receiverId)
    {
        var content =
            $"The user with email {this.Email} deleted her/his booking at {timeSlot.Start.ToString("dd/MM/yyyy")}.";
        var messageData = new MessageData($"Booking Deleted - {business.Name}", content);
        var metaData = new MessageMetadata(false, false);
        var message = new Message(Id.Value, receiverId, messageData, MessageType.BookingDeleted, metaData);
        this._sentMessage.Add(message);
    }
}
