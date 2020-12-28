using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class RegisterRequest : LoginRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Address { get; set; }

    }
}