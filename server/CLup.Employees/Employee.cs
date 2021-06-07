using System.ComponentModel.DataAnnotations;

using CLup.Businesses;
using CLup.Context;
using CLup.Users;

namespace CLup.Employees

{
    public class Employee : BaseEntity
    {
        public string Id { get; set; }
  
        public Business Business { get; set; }

        [Required]
        public string BusinessId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string CompanyEmail { get; set; }
    }
}