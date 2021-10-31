using System.Threading.Tasks;

using CLup.Domain.Users;

namespace CLup.Features.Users
{
    public interface IUserService
    {
        Task DetermineRole(User user);
    }
}