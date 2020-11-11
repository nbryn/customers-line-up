
using Logic.Businesses;

namespace Logic.DTO
{
    public class BusinessDTO : CreateBusinessDTO
    {

        public int Id { get; set; }

        public string OwnerEmail { get; set; }


        public BusinessDTO()
        {

        }

        public BusinessDTO(Business business)
        {
            Id = business.Id;
            OwnerEmail = business.OwnerEmail;
            Name = business.Name;
            Zip = business.Zip;

        }
    }
}