using System.Collections.Generic;

using CLup.Domain.ValueObjects;

namespace CLup.Domain
{
    public class User : BaseEntity
    {
        public UserData UserData { get; set; }

        public Address Address { get; set; }

        public Coords Coords { get; set; }

        public Role Role { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}