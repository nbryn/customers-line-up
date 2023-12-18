using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Domain.TimeSlots
{
    public class TimeSlot : Entity<TimeSlotId>, IHasDomainEvent
    {
        private List<BookingId> _bookingIds = new();

        public BusinessId BusinessId { get; private set; }

        public string BusinessName { get; private set; }

        public int Capacity { get; private set; }

        public DateTime Start { get; internal set; }

        public DateTime End { get; internal set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();

        public IReadOnlyList<BookingId> BookingIds => _bookingIds;

        public TimeSlot(
            BusinessId businessId,
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

        public bool IsAvailable() => BookingIds?.Count() < Capacity && (Start - DateTime.Now).TotalDays is < 14 and >= 0;
    }
}