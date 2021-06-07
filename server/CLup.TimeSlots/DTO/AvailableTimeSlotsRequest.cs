using System;
using System.ComponentModel.DataAnnotations;

namespace CLup.TimeSlots.DTO
{
    public class AvailableTimeSlotsRequest
    {
        [Required]
        public string BusinessId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required] 
        public DateTime End { get; set; }
    }
}