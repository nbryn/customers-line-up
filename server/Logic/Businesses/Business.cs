using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Logic.Context;
using Logic.Employees;
using Logic.TimeSlots;

namespace Logic.Businesses
{
    public class Business : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public BusinessType Type { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public string Opens { get; set; }

        [Required]
        public string Closes { get; set; }

        [Required]
        public int TimeSlotLength { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

    }
}