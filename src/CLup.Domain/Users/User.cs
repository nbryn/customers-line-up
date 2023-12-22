using System;
using System.Collections.Generic;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Shared.ValueObjects;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;
using CLup.Domain.Bookings;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Domain.Users;

public sealed class User : Entity, IAggregateRoot
{
    private readonly List<BusinessMessage> _receivedMessages = new();
    private readonly List<Business> _businessIds = new();
    private readonly List<UserMessage> _sentMessage = new();
    private readonly List<Booking> _bookings = new();

    public UserId Id { get; }

    public UserData UserData { get; private set; }

    public Address Address { get; private set; }

    public Coords Coords { get; private set; }

    public Role Role { get; set; }

    public IReadOnlyList<BusinessMessage> ReceivedMessages => _receivedMessages.AsReadOnly();

    public IReadOnlyList<UserMessage> SentMessages => _sentMessage.AsReadOnly();

    public IReadOnlyList<Business> BusinessIds => _businessIds.AsReadOnly();

    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

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

        Id = UserId.Create(Guid.NewGuid());
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

    public Booking? GetBookingById(BookingId bookingId) =>
        _bookings.Find(booking => booking.Id.Value == bookingId.Value);

    public bool BookingExists(TimeSlotId timeSlotId) =>
        _bookings.Exists(booking => booking.TimeSlot.Id.Value == timeSlotId.Value);

    public User Update(string name, string email, (Address address, Coords coords) info)
    {
        UserData = new UserData(name, email, Password);
        Address = info.address;
        Coords = info.coords;

        return this;
    }

    public void BookingDeletedMessage(Booking booking)
    {
        var content =
            $"The user with email {Email} deleted her/his booking at {booking.TimeSlot.Start.ToString("dd/MM/yyyy")}.";
        var messageData = new MessageData($"Booking Deleted - {booking.Business.Name}", content);
        var metaData = new MessageMetadata(false, false);
        var message = new UserMessage(Id, booking.Business.Id, messageData, MessageType.BookingDeleted, metaData);

        _sentMessage.Add(message);
    }
}
