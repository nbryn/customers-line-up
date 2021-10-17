using System;

using CLup.Domain;

namespace CLup.Context.Initialiser.DataCreators
{
    public static class BookingCreator
    {

        public static Booking Create(string userId, string businessId, string TimeSlotId)
        {
            Booking booking = new Booking
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                BusinessId = businessId,
                TimeSlotId = TimeSlotId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return booking;
        }
    }
}