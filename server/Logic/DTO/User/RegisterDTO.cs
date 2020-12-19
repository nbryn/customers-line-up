using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Zip { get; set; }

    }
}