using System;
using System.Collections.Generic;
using System.Linq;
using CLup.Domain.Bookings.ValueObjects;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Domain.TimeSlots
{
    using Bookings;

    public class TimeSlot : Entity<TimeSlotId>, IHasDomainEvent
    {
        private List<Booking> _bookings = new();

        public BusinessId BusinessId { get; private set; }

        public string BusinessName { get; private set; }

        public int Capacity { get; private set; }

        public DateTime Start { get; internal set; }

        public DateTime End { get; internal set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();

        public IReadOnlyList<Booking> Bookings => this._bookings.AsReadOnly();

        public TimeSlot(
            BusinessId businessId,
            string businessName,
            int capacity,
            DateTime start,
            DateTime end)
        {
            this.BusinessId = businessId;
            this.BusinessName = businessName;
            this.Capacity = capacity;
            this.Start = start;
            this.End = end;
        }

        public bool IsAvailable() => this.Bookings?.Count() < this.Capacity && (this.Start - DateTime.Now).TotalDays is < 14 and >= 0;
    }
}
