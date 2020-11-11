using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        public string Password {get; set;}

        [Required]
        public string Zip { get; set; }

    }
}