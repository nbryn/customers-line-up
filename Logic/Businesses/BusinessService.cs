using System.Threading.Tasks;

using Data;
using Logic.DTO;
using Logic.BusinessOwners;
using Logic.Util;
using Logic.DTO.User;

namespace Logic.Businesses
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessOwnerRepository _businessOwnerRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IDTOMapper _dtoMapper;



        public BusinessService(IBusinessOwnerRepository businessOwnerRepository,
        IBusinessRepository businessRepository, IDTOMapper dtoMapper)
        {
            _businessOwnerRepository = businessOwnerRepository;
            _businessRepository = businessRepository;
            _dtoMapper = dtoMapper;

        }
        public async Task<BusinessDTO> RegisterBusiness(CreateBusinessDTO business, string ownerEmail)
        {
            BusinessOwner owner = await _businessOwnerRepository.FindOwnerByEmail(ownerEmail);

            if (owner == null)
            {
                owner = await _businessOwnerRepository.CreateBusinessOwner(ownerEmail);
            }
            
            business.Owner = owner;

            Business newBusiness = await _businessRepository.CreateBusiness(business, ownerEmail);

            return _dtoMapper.ConvertBusinessToDTO(newBusiness);
        }
    }
}