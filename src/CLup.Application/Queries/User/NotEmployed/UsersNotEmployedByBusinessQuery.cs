using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.User.NotEmployed
{

    public class UsersNotEmployedByBusinessQuery : IRequest<Result<UsersNotEmployedByBusinessResponse>>
    {

        public string BusinessId { get; set; }
    }
}

