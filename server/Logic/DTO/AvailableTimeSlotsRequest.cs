using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.DTO
{
    public class AvailableTimeSlotsRequest
    {
        [Required]
        public int BusinessId { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required] 
        public DateTime End { get; set; }
    }
}