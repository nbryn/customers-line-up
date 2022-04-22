using System.Collections.Generic;

namespace CLup.Application.Users
{
    public class UsersNotEmployedByBusiness
    {
        public string BusinessId { get; init; }
        public IList<UserDto> Users { get; init; }
    }
}