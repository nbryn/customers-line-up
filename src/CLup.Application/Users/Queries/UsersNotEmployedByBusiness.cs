using System.Collections.Generic;

namespace CLup.Application.Users.Queries
{
    public class UsersNotEmployedByBusiness
    {
        public string BusinessId { get; init; }
        public IList<UserDto> Users { get; init; }
    }
}