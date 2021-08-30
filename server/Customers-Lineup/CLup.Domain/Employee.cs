namespace CLup.Domain
{
    public class Employee : BaseEntity
    {
  
        public Business Business { get; set; }

        public string BusinessId { get; set; }

        public User User { get; set; }

        public string UserEmail { get; set; }

        public string CompanyEmail { get; set; }
    }
}