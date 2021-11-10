using CLup.Domain.Shared;

namespace CLup.Domain.Business.Employee
{
    public class Employee : Entity
    {

        public string UserId { get; private set; }

        public User.User User { get; private set; }
  
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