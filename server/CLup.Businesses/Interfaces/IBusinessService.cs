using System.Threading.Tasks;
using System.Collections.Generic;

using CLup.Businesses.DTO;
using CLup.Util;

namespace CLup.Businesses.Interfaces
{
    public interface IBusinessService
    {
        Task<ServiceResponse> RegisterBusiness(BusinessRequest business);

        IEnumerable<string> GetBusinessTypes();
    }
}