using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CLup.Context;
using CLup.Employees;
using CLup.TimeSlots;

namespace CLup.Businesses
{
    public class Business : BaseEntity
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

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