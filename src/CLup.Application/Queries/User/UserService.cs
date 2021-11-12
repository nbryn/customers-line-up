using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User
{
    public class UserService : IUserService
    {
        private readonly IQueryDbContext _queryContext;

        public UserService(IQueryDbContext queryContext) => _queryContext = queryContext;

        public async Task DetermineRole(Domain.User.User user)
        {
            if (await _queryContext.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail == user.Email) != null)
            {
                user.Role = Role.Owner;

                return;
            }
            
            var isEmployee = await _queryContext.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (isEmployee != null)
            {
                user.Role = Role.Employee;

                return;
            }

            user.Role = Role.User;
        }
    }
}