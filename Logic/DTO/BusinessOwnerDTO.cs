using System.Collections.Generic;

namespace Logic.DTO
{
    public class BusinessOwnerDTO
    {

        public string UserEmail { get; set; }
        public IEnumerable<BusinessDTO> Businesses { get; set; }
    }
}