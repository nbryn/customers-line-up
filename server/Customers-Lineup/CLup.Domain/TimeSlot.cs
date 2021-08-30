using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLup.Domain
{
    public class TimeSlot : BaseEntity
    {

        public string BusinessId { get; set; }

        public Business Business { get; set; }

        public string BusinessName { get; set; }

        public int Capacity { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
        
        public IEnumerable<Booking> Bookings { get; set; }
    }
}