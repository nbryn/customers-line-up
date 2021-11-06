using System.Collections.Generic;

namespace CLup.Application.Users.Queries.Responses
{
    public class UsersNotEmployedByBusinessResponse
    {
        public string BusinessId { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}