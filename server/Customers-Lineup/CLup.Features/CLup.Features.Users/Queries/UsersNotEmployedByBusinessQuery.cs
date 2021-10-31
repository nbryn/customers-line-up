using MediatR;

using CLup.Features.Shared;
using CLup.Features.Users.Responses;

namespace CLup.Features.Users.Queries
{

    public class UsersNotEmployedByBusinessQuery : IRequest<Result<UsersNotEmployedByBusinessResponse>>
    {

        public string BusinessId { get; set; }
    }
}

