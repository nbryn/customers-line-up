using System.Threading.Tasks;
using System.Linq;

using Logic.Businesses;
using Logic.DTO;
using Logic.DTO.User;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<Business> CreateBusiness(CreateBusinessDTO business, string ownerEmail);

         Task<Business> FindBusinessById(int businessId);
    }
}