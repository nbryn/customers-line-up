using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}