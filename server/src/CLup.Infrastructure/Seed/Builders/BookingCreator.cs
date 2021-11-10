using System;
using CLup.Domain.Booking;

namespace CLup.Infrastructure.Seed.Builders
{
    public class BookingCreator
    {
        public static Booking Create(string userId, string businessId, string timeSlotId)
        {
            var booking = new Booking(userId, timeSlotId, businessId);
            booking.UpdatedAt = DateTime.Now;

            return booking;            
        }
    }
}