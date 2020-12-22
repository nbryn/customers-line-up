using System.ComponentModel.DataAnnotations;

namespace Logic.DTO
{
    public class NewEmployeeDTO
    {
        [Required]
        public int BusinessId { get; set; }
     
        [Required]
        public int PrivateEmail { get; set; }
        public int CompanyEmail { get; set; }
    }
}