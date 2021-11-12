using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Queries.User.Models
{

    public class UsersNotEmployedByBusinessQuery : IRequest<Result<UsersNotEmployedByBusinessResponse>>
    {

        public string BusinessId { get; set; }
    }
}

