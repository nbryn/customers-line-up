using System;
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
        
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
        
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

        public static IList<TimeSlot> GenerateTimeSlots(Business business, DateTime start)
        {
            var opens = start.AddHours(Double.Parse(business.Opens.Substring(0, business.Opens.IndexOf("."))));
            var closes = start.AddHours(Double.Parse(business.Closes.Substring(0, business.Closes.IndexOf("."))));

            var timeSlots = new List<TimeSlot>();
            for (var date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(business.BusinessData.TimeSlotLength))
            {
                var end = date.AddMinutes(business.BusinessData.TimeSlotLength);
                var timeSlot = new TimeSlot(business.Id, business.Name, business.BusinessData.Capacity, date, end);

                timeSlots.Add(timeSlot);
            }

            return timeSlots;
        }
    }
}