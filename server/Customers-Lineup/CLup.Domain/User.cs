using System.Collections.Generic;

namespace CLup.Domain
{
    public class User : BaseEntity
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Zip { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Role Role { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}