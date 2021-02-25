using System.Threading.Tasks;
using System.Collections.Generic;

using Logic.Businesses;
using Logic.DTO;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<HttpCode> CreateBusiness(NewBusinessRequest business);

         Task<Business> FindBusinessById(int businessId);

         Task<IList<Business>> FindBusinessesByOwner(string ownerEmail);

         Task<IList<Business>> GetAll();

         Task<HttpCode> UpdateBusiness(int businessId, NewBusinessRequest dto);
    }
}