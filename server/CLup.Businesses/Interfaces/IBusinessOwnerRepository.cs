using System.Threading.Tasks;

using CLup.Util;

namespace CLup.Businesses.Interfaces
{
    public interface IBusinessOwnerRepository
    {
       Task<ServiceResponse> CreateBusinessOwner(string ownerEmail);

       Task<BusinessOwner> FindOwnerByEmail(string ownerEmail);
 
    }
}