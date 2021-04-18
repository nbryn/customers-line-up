using System.ComponentModel.DataAnnotations;

namespace CLup.Users.DTO
{
    public class NewUserRequest : LoginRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}