using System;
using System.Collections.Generic;

namespace CLup.Application.Users.Queries;

public sealed class UsersNotEmployedByBusiness
{
    public Guid BusinessId { get; init; }
    public IList<UserDto> Users { get; init; }
}
