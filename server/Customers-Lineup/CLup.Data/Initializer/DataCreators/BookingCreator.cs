using System;

using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
{
    public static class BookingCreator
    {

        public static Booking Create(string userId, string businessId, string timeSlotId)
        {
            var booking = new Booking(userId, timeSlotId, businessId);
            booking.UpdatedAt = DateTime.Now;

            return booking;            
        }
    }
}