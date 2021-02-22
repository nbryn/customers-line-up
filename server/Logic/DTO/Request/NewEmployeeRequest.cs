using System.ComponentModel.DataAnnotations;

namespace Logic.DTO
{
    public class NewEmployeeRequest
    {
        [Required]
        public int BusinessId { get; set; }

        [Required]
        public string PrivateEmail { get; set; }
        public string? CompanyEmail { get; set; }
    }
}