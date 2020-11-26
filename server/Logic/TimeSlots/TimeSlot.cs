using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Logic.Bookings;

namespace Logic.TimeSlots
{
    public class TimeSlot
    {
        public int Id { get; set; }

        [Required]
        public int BusinessId { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}