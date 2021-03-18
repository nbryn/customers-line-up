using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Context;
using CLup.Extensions;
using CLup.Users.DTO;
using CLup.Users.Interfaces;
using CLup.Util;

namespace CLup.Users
{
    public class UserRepository : IUserRepository
    {      
        private readonly ICLupContext _context;
        private readonly IMapper _mapper;

        public UserRepository(
            ICLupContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<ServiceResponse<int>> CreateUser(NewUserRequest user)
        {
            User newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Zip = user.Zip,
                Address = user.Address,
                Longitude = user.Longitude,
                Latitude = user.Latitude,
            };

            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            return new ServiceResponse<int>(HttpCode.Created, newUser.Id);
        }

        public Task<User> FindUserById(int userId)
        {
            return null;
        }

        public async Task<ServiceResponse<IList<UserDTO>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return this.AssembleResponse<User, UserDTO>(users, _mapper);
        }

    }
}

