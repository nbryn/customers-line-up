using System.Threading.Tasks;

using Data;
using Logic.DTO;
using Logic.DTO.User;

namespace Logic.Businesses
{
    public class BusinessService : IBusinessService
    {

        private readonly IBusinessOwnerRepository _businessOwnerRepository;
        private readonly IBusinessRepository _businessRepository;


        public BusinessService(IBusinessOwnerRepository businessOwnerRepository, IBusinessRepository businessRepository)
        {
            _businessOwnerRepository = businessOwnerRepository;
            _businessRepository = businessRepository;
            
        }
        public async Task<BusinessDTO> RegisterBusiness(CreateBusinessDTO business, string ownerEmail)
        {
            await _businessOwnerRepository.CreateBusinessOwner(ownerEmail);
            BusinessDTO businessDTO = await _businessRepository.CreateBusiness(business, ownerEmail);

            return businessDTO;
        }
    }
}