using System.Threading.Tasks;


using Logic.Businesses;
using Logic.Businesses.Models;
using Logic.Users.Models;

namespace Data
{
    public interface IBusinessRepository
    {
         Task<int> Register(CreateBusinessDTO business, UserDTO owner);
    }
}