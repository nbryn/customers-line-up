namespace Logic.Businesses.Models
{
    public class BusinessDTO : CreateBusinessDTO
    {
        public int Id { get; set; }

        public string OwnerEmail { get; set; }
    }
}