using System.Threading.Tasks;

namespace CLup.Application.Queries.User
{
    public interface IUserService
    {
        Task DetermineRole(Domain.Users.User user);
    }
}