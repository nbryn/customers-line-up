using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
{
    public static class BookingCreator
    {

        public static Booking Create(string userId, string businessId, string timeSlotId)
        {
            return new Booking(userId, timeSlotId, businessId);            
        }
    }
}