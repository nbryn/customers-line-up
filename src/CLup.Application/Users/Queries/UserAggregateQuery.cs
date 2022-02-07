using CLup.Application.Shared;
using CLup.Application.Shared.Models;
using MediatR;

namespace CLup.Application.Users.Queries
{
    public class UserAggregateQuery : IRequest<Result<UserDto>>
    {
        public string UserEmail { get; set; }

        public UserAggregateQuery(string userEmail) => UserEmail = userEmail;
    }
}

