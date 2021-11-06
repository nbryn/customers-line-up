using CLup.Application.Shared;
using CLup.Application.Users.Queries.Responses;
using MediatR;

namespace CLup.Application.Users.Queries
{
    public class FetchMessagesQuery : IRequest<Result<FetchMessagesResponse>>
    {
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public FetchMessagesQuery(string userId) => UserId = userId;
    }
}

