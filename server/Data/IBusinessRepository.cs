using System.Threading.Tasks;
using System.Collections.Generic;

using Logic.Businesses;
using Logic.Context;
using Logic.DTO;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<Business> CreateBusiness(NewBusinessDTO business);

         Task<Business> FindBusinessById(int businessId);

         Task<IList<Business>> FindBusinessesByOwner(string ownerEmail);

         Task<IList<Business>> GetAll();

         Task<Response> UpdateBusiness(int businessId, NewBusinessDTO dto);
    }
}