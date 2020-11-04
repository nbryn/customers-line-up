using System.Threading.Tasks;

using Data;
using Logic.Businesses.Models;
using Logic.Users.Models;

namespace Logic.Businesses
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;

        public BusinessService(IBusinessRepository businessRepository, IUserRepository userRepository)
        {
            _businessRepository = businessRepository;
            _userRepository = userRepository;
        }
        public async Task<BusinessDTO> RegisterBusiness(string ownerEmail, CreateBusinessDTO business)
        {
            BusinessDTO businessDTO = await _businessRepository.CreateBusiness(business, ownerEmail);

            return businessDTO;
        }
    }
}