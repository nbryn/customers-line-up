using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CLup.Domain
{
    public class User : BaseEntity
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }
        
        public Role Role { get; set; }

        public IList<Booking> Bookings { get; set; }
    }
}