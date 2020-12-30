using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO.User;

namespace Logic.Users
{
    public interface IUserService
    {
        Task<LoginResponse> RegisterUser(RegisterUserDTO user);

        Task<LoginResponse> AuthenticateUser(LoginDTO user);

        Task<IList<User>> FilterUsersByBusiness(int businessId);

        Task<Role> DetermineRole(User user);

    }
}