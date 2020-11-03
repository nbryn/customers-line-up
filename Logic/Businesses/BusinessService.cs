using System.Threading.Tasks;

using Logic.Businesses.Models;

namespace Logic.Businesses
{
    public class BusinessService : IBusinessService
    {
        public Task<BusinessDTO> RegisterBusiness(string ownerEmail, CreateBusinessDTO business)
        {
            return null;
        }
    }
}