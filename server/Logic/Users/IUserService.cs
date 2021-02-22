using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO;
using Logic.DTO.User;

namespace Logic.Users
{
    public interface IUserService
    {
        Task<QueryResponse<UserDTO>> RegisterUser(NewUserRequest user);

        Task<QueryResponse<UserDTO>> AuthenticateUser(LoginRequest user);

        Task<IList<User>> FilterUsersByBusiness(int businessId);

        Task<Role> DetermineRole(User user);

    }
}