using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CLup.Bookings;
using CLup.Businesses;
using CLup.Context;

namespace CLup.TimeSlots
{
    public class TimeSlot : BaseEntity
    {
        public string Id { get; set; }

        [Required]
        public string BusinessId { get; set; }

        public Business Business { get; set; }

        public string BusinessName { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public IEnumerable<Booking>? Bookings { get; set; }
    }
}