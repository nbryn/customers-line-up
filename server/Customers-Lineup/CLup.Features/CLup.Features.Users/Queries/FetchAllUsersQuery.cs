using System.Collections.Generic;

using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Users.Queries
{
    public class FetchAllUsersQuery : IRequest<Result<IList<UserDTO>>>
    {

    }
}

