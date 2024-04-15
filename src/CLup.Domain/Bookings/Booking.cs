using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users;
using CLup.Domain.Users.ValueObjects;

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

    public List<DomainEvent> DomainEvents { get; set; } = [];

    public Booking(UserId userId, BusinessId businessId, TimeSlotId timeSlotId)
    {
        Guard.Against.Null(userId);
        Guard.Against.Null(businessId);
        Guard.Against.Null(timeSlotId);

        UserId = userId;
        BusinessId = businessId;
        TimeSlotId = timeSlotId;

        Id = BookingId.Create(Guid.NewGuid());
    }

    private Booking()
    {
    }
}
