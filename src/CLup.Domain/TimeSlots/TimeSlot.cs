using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.Shared;
using CLup.Domain.TimeSlots.ValueObjects;

namespace CLup.Domain.TimeSlots
{
    public class TimeSlot : Entity, IHasDomainEvent
    {
        private List<Booking> _bookings = new();

        public TimeSlotId Id { get; }

        public BusinessId BusinessId { get; private set; }

        public Business Business { get; private set; }

        public string BusinessName { get; private set; }

        public int Capacity { get; private set; }

        public DateTime Start { get; internal set; }

        public DateTime End { get; internal set; }

        public List<DomainEvent> DomainEvents { get; set; } = new();

        public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();

        protected TimeSlot()
        {
        }

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

            Id = TimeSlotId.Create(Guid.NewGuid());
        }

        public DomainResult IsAvailable()
        {
            if (Bookings.Count >= Capacity)
            {
                return DomainResult.Fail(new[] { TimeSlotErrors.NoCapacity });
            }

            if (DateOnly.FromDateTime(Start) < DateOnly.FromDateTime(DateTime.Now))
            {
                return DomainResult.Fail(new[] { TimeSlotErrors.InThePast });
            }

            return DomainResult.Ok();
        }

        public string FormatInterval() =>
            $"{Start.TimeOfDay.ToString().Substring(0, 5)} - {End.TimeOfDay.ToString().Substring(0, 5)}";
    }
}
