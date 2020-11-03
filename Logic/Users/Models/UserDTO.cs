using System.ComponentModel.DataAnnotations;

namespace Logic.Users.Models
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
        public string Password { get; set; }

        [Required]
        public string Zip { get; set; }

    }
}