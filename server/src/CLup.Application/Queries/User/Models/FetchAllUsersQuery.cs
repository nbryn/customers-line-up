using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.User.Models
{
    public class FetchAllUsersQuery : IRequest<Result<IList<UserDto>>>
    {

    }
}

