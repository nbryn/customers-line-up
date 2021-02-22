using System.Threading.Tasks;

using Logic.DTO;

namespace Logic.Businesses
{
    public interface IBusinessService
    {
        Task<HttpCode> RegisterBusiness(NewBusinessRequest business);

    }
}