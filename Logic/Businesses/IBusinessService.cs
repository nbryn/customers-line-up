using System.Threading.Tasks;

using Logic.Businesses.Models;

namespace Logic.Businesses
{
    public interface IBusinessService
    {
        Task<BusinessDTO> RegisterBusiness(string ownerEmail, CreateBusinessDTO business);

    }
}