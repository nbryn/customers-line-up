using System.Collections.Generic;

namespace CLup.Features.Users.Responses
{
    public class UsersNotEmployedByBusinessResponse
    {
        public string BusinessId { get; set; }
        public IList<UserDTO> Users { get; set; }
    }
}