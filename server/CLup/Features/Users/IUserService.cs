using System.Threading.Tasks;

namespace CLup.Users.Interfaces
{
    public interface IUserService
    {
        Task DetermineRole(User user);
    }
}