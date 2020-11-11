using System;
using System.Collections.Generic;

using Logic.Users;
using Logic.Businesses;

namespace Logic.BusinessQueues
{
    public class BusinessQueue
    {
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public int Capacity { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ICollection<User> Customers { get; set; }
    }
}