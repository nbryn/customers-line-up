using System;
using System.Linq;
using System.Collections.Generic;
using CLup.Domain.Bookings;
using CLup.Domain.Shared;

namespace CLup.Domain.Businesses.TimeSlots
{
    public class TimeSlot : Entity, IHasDomainEvent
    {
        public string BusinessId { get; private set; }

        public Business Business { get; private set; }

        public string BusinessName { get; private set; }

        public int Capacity { get; private set; }

        public DateTime Start { get; internal set; }

        public DateTime End { get; internal set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();

        public IEnumerable<Booking> Bookings { get; private set; }

        public TimeSlot(
            string businessId,
            string businessName,
            int capacity,
            DateTime start,
            DateTime end)
        {
            BusinessId = businessId;
            BusinessName = businessName;
            Capacity = capacity;
            Start = start;
            End = end;
        }

        public bool IsAvailable() => Bookings?.Count() < Capacity && (Start - DateTime.Now).TotalDays is < 14 and >= 0;
    }
}