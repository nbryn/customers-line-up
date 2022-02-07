using System.Collections.Generic;
using CLup.Application.Shared.Models;

namespace CLup.Application.Users.Queries.NotEmployed
{
    public class UsersNotEmployedByBusinessResponse
    {
        public string BusinessId { get; set; }
        public IList<UserDto> Users { get; set; }
    }
}