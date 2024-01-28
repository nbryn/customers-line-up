using System;
using System.Collections.Generic;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;

namespace CLup.Domain.Bookings;

public sealed class Booking : Entity, IHasDomainEvent
{
    public BookingId Id { get; }

    public UserId UserId { get; private set; }

    public User User { get; private set; }

    public TimeSlotId TimeSlotId { get; private set; }

    public TimeSlot TimeSlot { get; private set; }

    public BusinessId BusinessId { get; private set; }

    public Business Business { get; private set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    protected Booking()
    {
    }

    public Booking(UserId userId, BusinessId businessId, TimeSlotId timeSlotId)
    {
        UserId = userId;
        BusinessId = businessId;
        TimeSlotId = timeSlotId;

        Id = BookingId.Create(Guid.NewGuid());
    }
}
