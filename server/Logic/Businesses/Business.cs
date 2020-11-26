using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Logic.BusinessOwners;
using Logic.TimeSlots;

namespace Logic.Businesses
{
    public class Business
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public string Zip { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public double OpeningTime { get; set; }

        public double ClosingTime { get; set; }

        public IEnumerable<TimeSlot> Queues { get; set; }
    }
}