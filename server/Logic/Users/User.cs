using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using Logic.Bookings;

namespace Logic.Users
{
    public class User
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
        public int Zip { get; set; }

        public IList<Booking> Bookings { get; set; }


    }
}