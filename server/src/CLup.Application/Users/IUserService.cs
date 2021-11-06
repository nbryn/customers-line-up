using System.Threading.Tasks;
using CLup.Domain.Users;

namespace CLup.Application.Users
{
    public interface IUserService
    {
        Task DetermineRole(User user);
    }
}