using System.ComponentModel.DataAnnotations;

namespace Logic.Models
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; }

        [Required]
        public string Password { get; }
    }
}