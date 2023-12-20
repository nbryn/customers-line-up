using System.Collections.Generic;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Domain.Bookings;

using Businesses;
using TimeSlots;
using Users;

public sealed class Booking : Entity<BookingId>, IHasDomainEvent
{
    public UserId UserId { get; private set; }

    public User User { get; private set; }

    public TimeSlotId TimeSlotId { get; private set; }

    public TimeSlot TimeSlot { get; private set; }

    public BusinessId BusinessId { get; private set; }

    public Business Business { get; private set; }

    public List<DomainEvent> DomainEvents { get; set; } = new();

    public Booking(UserId userId, TimeSlotId timeSlotId, BusinessId businessId)
    {
        this.UserId = userId;
        this.TimeSlotId = timeSlotId;
        this.BusinessId = businessId;
    }
}
