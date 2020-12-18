using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int Zip { get; set; }

        public BusinessType Type { get; set; }

        public int Capacity { get; set; }

        public string Opens { get; set; }

        public string Closes { get; set; }

        public int TimeSlotLength { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }
    }
}