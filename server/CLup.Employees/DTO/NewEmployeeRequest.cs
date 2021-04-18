using System.ComponentModel.DataAnnotations;

namespace CLup.Employees.DTO
{
    public class NewEmployeeRequest
    {
        [Required]
        public int BusinessId { get; set; }

        [Required]
        public string PrivateEmail { get; set; }
        
        #nullable enable
        public string? CompanyEmail { get; set; }
    }
}