namespace CLup.Domain
{
    public class Employee : BaseEntity
    {

        public string UserId { get; private set; }

        public User User { get; private set; }
  
        public Business Business { get; private set; }

        public string BusinessId { get; private set; }

        public string CompanyEmail { get; private set; }

        public Employee(string userId, string businessId, string companyEmail)
            : base()
        {
            UserId = userId;
            BusinessId = businessId;
            CompanyEmail = companyEmail;
        }

    }
}