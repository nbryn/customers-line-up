using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.DTO.User;

namespace Logic.Users
{
    public interface IUserService
    {
        Task<LoginResponse> RegisterUser(RegisterRequest user);

        Task<LoginResponse> AuthenticateUser(LoginRequest user);

        Task<IList<User>> FilterUsersByBusiness(int businessId);

        Task<Role> DetermineRole(User user);

    }
}