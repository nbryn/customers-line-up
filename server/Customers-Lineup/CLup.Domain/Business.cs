using System.Collections.Generic;

namespace CLup.Domain
{
    public class Business : BaseEntity
    {

        public string Name { get; set; }

        public string OwnerEmail { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public BusinessType Type { get; set; }

        public int Capacity { get; set; }

        public string Opens { get; set; }

        public string Closes { get; set; }

        public int TimeSlotLength { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

    }
}