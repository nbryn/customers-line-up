using System.Collections.Generic;

using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Users.Queries
{
    public class FetchAllUsersQuery : IRequest<Result<IList<UserDTO>>>
    {

    }
}

