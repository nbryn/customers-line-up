using System.ComponentModel.DataAnnotations;

namespace Logic.Users.Models
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Zip { get; set; }

    }
}