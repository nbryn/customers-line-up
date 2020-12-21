using System;
using System.ComponentModel.DataAnnotations;

using Logic.Businesses;
using Logic.Users;

namespace Logic.Employees

{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        
        [Required]
        public Business Business { get; set; }

        [Required]
        public int BusinessId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string CompanyEmail { get; set; }
    }
}