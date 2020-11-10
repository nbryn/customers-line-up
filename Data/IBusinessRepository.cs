using System.Threading.Tasks;
using System.Linq;

using Logic.Businesses;
using Logic.Businesses.Models;
using Logic.Users.Models;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<BusinessDTO> CreateBusiness(CreateBusinessDTO business, string ownerEmail);

        
         IQueryable<BusinessDTO> Read();
    }
}