using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Businesses.DTO;
using CLup.Context;
using CLup.Util;

namespace CLup.Businesses.Interfaces
{
    public interface IBusinessRepository : IRepository<Business>
    {
         Task<ServiceResponse> CreateBusiness(BusinessRequest business);

         Task<Business> FindBusinessById(string businessId);

         Task<ServiceResponse<IList<BusinessDTO>>> FindBusinessesByOwner(string ownerEmail);

         Task<ServiceResponse<IList<BusinessDTO>>> GetAll();

         Task<ServiceResponse> UpdateBusiness(string businessId, BusinessRequest dto);
    }
}