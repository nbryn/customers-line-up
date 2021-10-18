using System;

using CLup.Domain;

namespace CLup.Data.Initializer.DataCreators
{

    public static class TimeSlotCreator
    {

        public static TimeSlot Create(
            string id, string businessId, string businessName,
            int capacity, DateTime start, DateTime end)
        {
            TimeSlot TimeSlot = new TimeSlot
            {
                Id = id,
                BusinessId = businessId,
                BusinessName = businessName,
                Capacity = capacity,
                Start = start,
                End = end,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return TimeSlot;
        }
    }
}