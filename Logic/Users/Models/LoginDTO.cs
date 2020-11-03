using System.ComponentModel.DataAnnotations;

namespace Logic.Users.Models
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}