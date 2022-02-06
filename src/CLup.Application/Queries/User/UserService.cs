using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Domain.User;
using CLup.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User
{
    public class UserService : IUserService
    {
        private readonly IReadOnlyDbContext _readOnlyContext;

        public UserService(IReadOnlyDbContext readOnlyContext) => _readOnlyContext = readOnlyContext;

        public async Task DetermineRole(Domain.Users.User user)
        {
            if (await _readOnlyContext.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail == user.Email) != null)
            {
                user.Role = Role.Owner;

                return;
            }
            
            var isEmployee = await _readOnlyContext.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (isEmployee != null)
            {
                user.Role = Role.Employee;

                return;
            }

            user.Role = Role.User;
        }
    }
}