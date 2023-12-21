using System;
using System.Collections.Generic;
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

using System.Linq;
using Bookings;
using Bookings.ValueObjects;
using TimeSlots.ValueObjects;

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

    public IReadOnlyList<Message> ReceivedMessages => _receivedMessages.AsReadOnly();

    public IReadOnlyList<Message> SentMessages => _sentMessage.AsReadOnly();

    public IReadOnlyList<BusinessId> BusinessIds => _businessIds.AsReadOnly();

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

    public Booking GetBookingById(BookingId bookingId) =>
        _bookings.Find(booking => booking.Id.Value == bookingId.Value);

    public bool BookingExists(TimeSlotId timeSlotId) =>
        _bookings.Exists(booking => booking.TimeSlotId.Value == timeSlotId.Value);

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
        var message = new Message(Id.Value, booking.Business.Id.Value, messageData, MessageType.BookingDeleted, metaData);
        _sentMessage.Add(message);
    }
}
