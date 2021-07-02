using System.Collections.Generic;

namespace CLup.Domain
{
    public class BusinessOwner : BaseEntity
    {
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public ICollection<Business> Businesses { get; set; }

    }
}