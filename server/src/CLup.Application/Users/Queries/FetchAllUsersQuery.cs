using System.Collections.Generic;
using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Users.Queries
{
    public class FetchAllUsersQuery : IRequest<Result<IList<UserDto>>>
    {

    }
}

