using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.DTO
{
    public class GenerateTimeSlotsRequest
    {
        [Required]
        public int BusinessId { get; set; }

        [Required]
        public DateTime Start { get; set; }
    }
}