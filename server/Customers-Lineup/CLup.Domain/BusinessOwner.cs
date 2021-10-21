using System.Collections.Generic;

namespace CLup.Domain
{
    public class BusinessOwner : BaseEntity
    {
        public string UserEmail { get; private set; }

        public ICollection<Business> Businesses { get; private set; }

        public BusinessOwner(string userEmail) : base()
        {
            UserEmail = userEmail;
        }
    }
}