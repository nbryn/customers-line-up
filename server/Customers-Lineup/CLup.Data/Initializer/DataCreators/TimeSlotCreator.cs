using System;

using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
{

    public static class TimeSlotCreator
    {

        public static TimeSlot Create(
            string businessId, 
            string businessName,
            int capacity, 
            DateTime start, 
            DateTime end)
        {
            var timeSlot = new TimeSlot(businessId, businessName, capacity, start, end);
            timeSlot.UpdatedAt = DateTime.Now;

            return timeSlot;
        }
    }
}