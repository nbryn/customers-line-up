using System;
using System.ComponentModel.DataAnnotations;

namespace CLup.TimeSlots.DTO
{
    public class GenerateTimeSlotsRequest
    {
        [Required]
        public string BusinessId { get; set; }

        [Required]
        public DateTime Start { get; set; }
    }
}