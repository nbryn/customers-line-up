using System.ComponentModel.DataAnnotations;

using Logic.Businesses;
using Logic.Context;
using Logic.Users;

namespace Logic.Employees

{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
  
        public Business Business { get; set; }

        [Required]
        public int BusinessId { get; set; }

        public User User { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string CompanyEmail { get; set; }
    }
}