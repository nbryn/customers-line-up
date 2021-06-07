using System;
using CLup.Bookings;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class BookingCreator
    {

        public static Booking Create(string userEmail, string businessId, string TimeSlotId)
        {
            Booking booking = new Booking
            {
                UserEmail = userEmail,
                BusinessId = businessId,
                TimeSlotId = TimeSlotId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return booking;
        }
    }
}