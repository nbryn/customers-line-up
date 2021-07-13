using System.ComponentModel.DataAnnotations;

namespace CLup.Domain
{
    public class Employee : BaseEntity
    {
  
        public Business Business { get; set; }

        [Required]
        public string BusinessId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string CompanyEmail { get; set; }
    }
}