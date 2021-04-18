using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Businesses.Interfaces;
using CLup.Businesses.DTO;
using CLup.Util;

namespace CLup.Businesses
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessOwnerRepository _businessOwnerRepository;
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(
            IBusinessOwnerRepository businessOwnerRepository,
            IBusinessRepository businessRepository)
        {
            _businessOwnerRepository = businessOwnerRepository;
            _businessRepository = businessRepository;

        }
        public async Task<ServiceResponse> RegisterBusiness(BusinessRequest business)
        {
            BusinessOwner owner = await _businessOwnerRepository.FindOwnerByEmail(business.OwnerEmail);

            if (owner == null)
            {
                await _businessOwnerRepository.CreateBusinessOwner(business.OwnerEmail);
            }

            var response = await _businessRepository.CreateBusiness(business);

            return response;
        }

        public IEnumerable<string> GetBusinessTypes()
        {
            List<string> values = new List<string>();
            var types = EnumUtil.GetValues<BusinessType>();

            foreach (BusinessType type in types)
            {
                values.Add(type.ToString("G"));
            }

            return values;
        }
    }
}