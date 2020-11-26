using System.Threading.Tasks;

using Logic.BusinessOwners;
using Logic.DTO;

namespace Data
{
    public interface IBusinessOwnerRepository
    {
       Task<BusinessOwner> CreateBusinessOwner(string ownerEmail);

       Task<BusinessOwner> FindOwnerByEmail(string ownerEmail);
 
    }
}