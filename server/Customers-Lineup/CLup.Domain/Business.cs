using System.Collections.Generic;

using CLup.Domain.ValueObjects;

namespace CLup.Domain
{
    public class Business : BaseEntity
    {

        public string OwnerEmail { get; set; }

        public BusinessData BusinessData { get; set; }

        public Address Address { get; set; }

        public Coords Coords {get; set; }

        public TimeSpan BusinessHours { get; set; }

        public BusinessType Type { get; set; }

        public IEnumerable<TimeSlot> TimeSlots { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}