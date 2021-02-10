using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }

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
    }
}