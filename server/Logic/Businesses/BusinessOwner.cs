using System.Collections.Generic;

using Logic.Businesses;

namespace Logic.BusinessOwners
{
    public class BusinessOwner
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public ICollection<Business> Businesses { get; set; }

    }
}