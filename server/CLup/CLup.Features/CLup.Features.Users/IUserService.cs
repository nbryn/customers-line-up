using System.Threading.Tasks;

using CLup.Domain;

namespace CLup.Features.Users
{
    public interface IUserService
    {
        Task DetermineRole(User user);
    }
}