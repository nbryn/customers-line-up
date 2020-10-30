using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Logic.Entities;
using Logic.Models;

namespace Data
{
    public class UserRepository : IUserRepository
    {

        private readonly ICLupContext _context;

        public UserRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<int> Create(UserDTO user)
        {
            User newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Zip = user.Zip
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public Task<UserDTO> Read(int userId)
        {
            return null;
        }

        public IEnumerable<UserDTO> Read()
        {
            return from u in _context.Users
                   select new UserDTO
                   {
                       Name = u.Name,
                       Email = u.Email,
                       Password = u.Password,
                       Zip = u.Zip
                   };
        }
    }
}

