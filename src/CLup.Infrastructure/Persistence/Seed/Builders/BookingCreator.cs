using CLup.Domain.Bookings;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.TimeSlots.ValueObjects;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Infrastructure.Persistence.Seed.Builders;

public static class BookingCreator
{
    public static Booking Create(UserId userId, BusinessId businessId, TimeSlotId timeSlotId)
    {
        var booking = new Booking(userId, businessId, timeSlotId);
        booking.UpdatedAt = DateTime.Now;

        return booking;
    }
}
