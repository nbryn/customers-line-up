using System.Collections.Generic;

using CLup.Domain.Shared;

namespace CLup.Domain.Businesses
{
    public class BusinessOwner : Entity
    {
        public string UserEmail { get; private set; }

        public ICollection<Business> Businesses { get; private set; }

        public BusinessOwner(string userEmail) : base()
        {
            UserEmail = userEmail;
        }
    }
}