using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public bool isOwner { get; set; }

        public bool isError { get; set; }
    }
}