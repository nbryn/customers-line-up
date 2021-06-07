using System.ComponentModel.DataAnnotations;

namespace CLup.Users.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        [Required]
        public string Role { get; set; }

        public string Token { get; set; }
    }
}