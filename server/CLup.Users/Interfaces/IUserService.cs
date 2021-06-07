using System.Collections.Generic;
using System.Threading.Tasks;

using CLup.Users.DTO;
using CLup.Util;

namespace CLup.Users.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDTO>> RegisterUser(NewUserRequest user);

        Task<ServiceResponse<UserDTO>> AuthenticateUser(LoginRequest user);

        Task<ServiceResponse<IList<UserDTO>>> FilterUsersByBusiness(string businessId);

        Task DetermineRole(User user);

    }
}