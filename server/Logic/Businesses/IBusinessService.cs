using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.Businesses
{
    public interface IBusinessService
    {
        Task<BusinessDTO> RegisterBusiness(CreateBusinessDTO business);

    }
}