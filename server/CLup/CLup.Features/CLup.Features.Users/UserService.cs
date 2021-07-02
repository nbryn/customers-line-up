using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using CLup.Data;
using CLup.Domain;

namespace CLup.Features.Users
{
    public class UserService : IUserService
    {
        private readonly CLupContext _context;

        public UserService(CLupContext context) => _context = context;

        public async Task DetermineRole(User user)
        {
            if (await _context.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail == user.Email) != null)
            {
                user.Role = Role.Owner;

                return;
            }
            var isEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.UserEmail == user.Email);

            if (isEmployee != null)
            {
                user.Role = Role.Employee;

                return;
            }

            user.Role = Role.User;
        }
    }
}