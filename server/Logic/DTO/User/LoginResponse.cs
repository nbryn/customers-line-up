using System.ComponentModel.DataAnnotations;

namespace Logic.DTO.User
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        public bool isError { get; set; }
    }
}