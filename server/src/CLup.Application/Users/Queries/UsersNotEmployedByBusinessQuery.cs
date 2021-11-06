using CLup.Application.Shared;
using CLup.Application.Users.Queries.Responses;
using MediatR;

namespace CLup.Application.Users.Queries
{

    public class UsersNotEmployedByBusinessQuery : IRequest<Result<UsersNotEmployedByBusinessResponse>>
    {

        public string BusinessId { get; set; }
    }
}

