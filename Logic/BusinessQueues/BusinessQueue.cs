using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Logic.Users;
using Logic.Businesses;

namespace Logic.BusinessQueues
{
    public class BusinessQueue
    {
        public int Id { get; set; }
        [Required]
        public int BusinessId { get; set; }
        [Required]
        public Business Business { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public double Length { get; set; }

        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public ICollection<User>? Customers { get; set; }
    }
}