using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Logic.Users;
using Logic.Context;
using Logic.DTO.User;
using Logic.DTO;

namespace Data
{
    public class UserRepository : IUserRepository
    {

        private readonly ICLupContext _context;

        public UserRepository(ICLupContext context)
        {
            _context = context;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _context.Users.FindAsync(email);
        }
        public async Task<int> CreateUser(RegisterDTO user)
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

        public Task<User> FindUserById(int userId)
        {
            return null;
        }

       public async Task<IList<User>> GetAll()
       {
           return await _context.Users.ToListAsync();
       }

    }
}

