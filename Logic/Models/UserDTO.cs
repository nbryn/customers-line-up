using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class UserDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Zip { get; set; }
    }
}