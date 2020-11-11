using System.Threading.Tasks;
using System.Linq;

using Logic.Businesses;
using Logic.DTO;
using Logic.DTO.User;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<BusinessDTO> CreateBusiness(CreateBusinessDTO business, string ownerEmail);
         IQueryable<BusinessDTO> Read();
    }
}