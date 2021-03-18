using CLup.Bookings;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class BookingCreator
    {

        public static Booking Create(string userEmail, int businessId, int TimeSlotId)
        {
            Booking booking = new Booking
            {
               UserEmail = userEmail,
               BusinessId = businessId,
               TimeSlotId = TimeSlotId
            };

            return booking;
        }
    }
}