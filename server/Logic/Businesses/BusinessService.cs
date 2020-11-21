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
        public async Task<BusinessDTO> RegisterBusiness(CreateBusinessDTO business)
        {
            BusinessOwner owner = await _businessOwnerRepository.FindOwnerByEmail(business.OwnerEmail);

            if (owner == null)
            {
                await _businessOwnerRepository.CreateBusinessOwner(business.OwnerEmail);
            }

            Business newBusiness = await _businessRepository.CreateBusiness(business);

            return _dtoMapper.ConvertBusinessToDTO(newBusiness);
        }
    }
}