using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<UserDTO> FindByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email)
                                       .Select(u =>
                                            new UserDTO
                                            {
                                                Id = u.Id,
                                                Name = u.Name,
                                                Email = u.Email,
                                                Password = u.Password,
                                                Zip = u.Zip
                                            })
                                        .FirstOrDefaultAsync();
        }
        public async Task<int> Register(RegisterDTO user)
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

        public IQueryable<UserDTO> Read()
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

