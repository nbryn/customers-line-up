using System.Collections.Generic;

using Logic.Businesses.Models;

namespace Logic.BusinessOwners.Models
{
    public class BusinessOwnerDTO
    {

        public string UserEmail { get; set; }
        public IEnumerable<BusinessDTO> Businesses { get; set; }
    }
}