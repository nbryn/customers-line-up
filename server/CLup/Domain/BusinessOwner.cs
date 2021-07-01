using System.Collections.Generic;

using CLup.Context;
    
namespace CLup.Businesses
{
    public class BusinessOwner : BaseEntity
    {
        public string Id { get; set; }

        public string UserEmail { get; set; }

        public ICollection<Business> Businesses { get; set; }

    }
}