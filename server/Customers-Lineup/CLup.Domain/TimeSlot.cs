using System;
using System.Collections.Generic;

namespace CLup.Domain
{
    public class TimeSlot : BaseEntity
    {

        public string BusinessId { get; private set; }

        public Business Business { get; private set; }

        public string BusinessName { get; private set; }

        public int Capacity { get; private set; }

        public DateTime Start { get; internal set; }

        public DateTime End { get; internal set; }
        
        public IEnumerable<Booking> Bookings { get; private set; }

        public TimeSlot(
                string businessId,
                string businessName,
                int capacity,
                DateTime start,
                DateTime end)
            : base()
        {
            BusinessId = businessId;
            BusinessName = businessName;
            Capacity = capacity;
            Start = start;
            End = end;
        }
    }
}