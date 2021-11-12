using System.Collections.Generic;
using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Queries.User.General
{
    public class FetchAllUsersQuery : IRequest<Result<IList<UserDto>>>
    {

    }
}

