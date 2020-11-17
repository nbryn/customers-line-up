using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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