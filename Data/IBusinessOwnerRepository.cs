using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using Logic.BusinessOwners;
using Logic.DTO;

namespace Data
{
    public interface IBusinessOwnerRepository
    {
       Task<int> CreateBusinessOwner(string ownerEmail);

       IQueryable<BusinessOwnerDTO> Read();

 
    }
}