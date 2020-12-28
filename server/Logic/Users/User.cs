using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Logic.Bookings;
using Logic.Context;

namespace Logic.Users
{
    public class User : BaseEntity
    {
        public int Id { get; set; }

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

        public IList<Booking> Bookings { get; set; }

    }
}