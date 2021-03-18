using System;

using CLup.TimeSlots;

namespace CLup.Context.Initialiser.DataCreators
{

    public static class TimeSlotCreator
    {

        public static TimeSlot Create(int businessId, string businessName, int capacity, DateTime start, DateTime end)
        {
            TimeSlot TimeSlot = new TimeSlot
            {
                BusinessId = businessId,
                BusinessName = businessName,
                Capacity = capacity,
                Start = start,
                End = end
            };

            return TimeSlot;
        }
    }
}