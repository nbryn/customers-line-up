using System.Collections.Generic;

namespace CLup.Businesses.DTO
{
    public class BusinessOwnerDTO
    {
        public string UserEmail { get; set; }
        public IEnumerable<BusinessDTO> Businesses { get; set; }
    }
}