
using Logic.Businesses;

namespace Logic.DTO
{
    public class BusinessDTO : CreateBusinessDTO
    {
        public int Id { get; set; }

        public string OwnerEmail { get; set; }

    }
}