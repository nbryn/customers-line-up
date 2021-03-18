using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Context;
using CLup.Users.DTO;
using CLup.Util;

namespace CLup.Users.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByEmail(string email);
        Task<ServiceResponse<int>> CreateUser(NewUserRequest user);
        Task<User> FindUserById(int userId);
        Task<ServiceResponse<IList<UserDTO>>> GetAll();

    }
}