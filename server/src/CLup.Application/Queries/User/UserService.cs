using System.Threading.Tasks;
using CLup.Domain.User;
using CLup.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CLup.Application.Queries.User
{
    public class UserService : IUserService
    {
        private readonly CLupContext _context;

        public UserService(CLupContext context) => _context = context;

        public async Task DetermineRole(Domain.User.User user)
        {
            if (await _context.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail == user.Email) != null)
            {
                user.Role = Role.Owner;

                return;
            }
            
            var isEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == user.Id);

            if (isEmployee != null)
            {
                user.Role = Role.Employee;

                return;
            }

            user.Role = Role.User;
        }
    }
}