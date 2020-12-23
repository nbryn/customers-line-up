using System.Collections.Generic;

using Logic.Businesses;
using Logic.Context;
namespace Logic.BusinessOwners
{
    public class BusinessOwner : BaseEntity
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public ICollection<Business> Businesses { get; set; }

    }
}